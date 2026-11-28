using ElectronicsStore.Data;
using ElectronicsStore.Entities;
using ElectronicsStore.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ElectronicsStore.Repositories;

public sealed class OrderRepository(IDbContextFactory<DataContext> factory, ILogger<OrderRepository> logger)
    : IOrderRepository
{
    public IEnumerable<Order> GetAll() => Execute(nameof(GetAll), null, db =>
    {
        var orders = ReadOrders(db).OrderByDescending(o => o.OrderDate).ThenByDescending(o => o.Id).ToList();
        logger.LogDebug("Получено заказов: {Count}", orders.Count);
        return orders;
    });

    public Order? GetById(int id) => Execute(nameof(GetById), id, db =>
    {
        var order = ReadOrders(db).SingleOrDefault(o => o.Id == id);
        logger.LogDebug("Заказ {OrderId} найден: {Found}", id, order is not null);
        return order;
    });

    public void Add(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        Execute(nameof(Add), order.Id, db =>
        {
            logger.LogDebug("Оформление заказа для клиента {CustomerId}, доставка {Delivery}, строк {Count}",
                order.CustomerId, order.Delivery, order.Items.Count);
            if (order.Id != 0 || order.Status != OrderStatus.Processing)
                throw new InvalidOperationException("Оформить можно только новый заказ.");
            if (order.Items.Count == 0)
                throw new InvalidOperationException("Добавьте хотя бы один товар.");

            var requested = order.Items.GroupBy(item => item.ProductId)
                .ToDictionary(group => group.Key, group => group.Sum(item => (long)item.Quantity));
            using var transaction = db.Database.BeginTransaction();

            var customer = db.Customers.FromSqlInterpolated($"""
                SELECT c.*, c.xmin FROM "Customers" AS c
                WHERE c."Id" = {order.CustomerId} AND NOT c."IsArchived"
                FOR SHARE
                """).ToList().SingleOrDefault()
                ?? throw new InvalidOperationException("Клиент не найден или находится в архиве.");

            var ids = requested.Keys.ToArray();
            var products = db.Products.Where(p => ids.Contains(p.Id) && !p.IsArchived).ToDictionary(p => p.Id);
            foreach (var (id, quantity) in requested)
            {
                if (!products.TryGetValue(id, out var product))
                    throw new InvalidOperationException($"Товар №{id} не найден или находится в архиве.");
                if (quantity > product.StockQuantity)
                    throw new InvalidOperationException($"Недостаточно товара «{product.Name}» на складе: доступно {product.StockQuantity}, запрошено {quantity}.");
                product.RemoveFromStock((int)quantity);
            }

            var saved = order.PrepareForCheckout(customer, products);
            db.Orders.Add(saved);
            db.SaveChanges();
            transaction.Commit();
            order.CompleteCheckout(saved);
            logger.LogInformation("Оформлен заказ {OrderId} для клиента {CustomerId}, сумма {Total}",
                order.Id, order.CustomerId, order.TotalAmount);
            return order.Id;
        });
    }

    public void UpdateStatus(int id, OrderStatus status) => Execute(nameof(UpdateStatus), id, db =>
    {
        logger.LogDebug("Запрошен статус {Status} для заказа {OrderId}", status, id);
        var order = db.Orders.SingleOrDefault(o => o.Id == id)
            ?? throw new InvalidOperationException($"Заказ №{id} не найден.");
        var previous = order.Status;
        order.TransitionTo(status);
        var count = db.SaveChanges();
        logger.LogInformation("Заказ {OrderId}: {PreviousStatus} → {Status}", id, previous, status);
        return count;
    });

    private static IQueryable<Order> ReadOrders(DataContext db) => db.Orders
        .AsNoTrackingWithIdentityResolution().Include(o => o.Customer).Include(o => o.Items).ThenInclude(i => i.Product);

    private T Execute<T>(string operation, int? id, Func<DataContext, T> action) =>
        RepositoryOperation.Run(factory, logger, $"Order.{operation}", id, action);
}
