using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicsStore.Enums;

namespace ElectronicsStore.Entities;

public class Order
{
    [Key]
    public int Id { get; private set; }
    public DateTime OrderDate { get; private set; }
    public DateTime? ShippedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public int CustomerId { get; private set; }
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; private set; } = null!;
    [Required, MaxLength(200)]
    public string CustomerName { get; private set; } = string.Empty;
    public OrderStatus Status { get; private set; }
    public DeliveryOptions Delivery { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    [NotMapped]
    public decimal TotalAmount => _items.Sum(item => item.LineTotal);

    private Order() { }

    public static Order Create(int id, int customerId, DeliveryOptions delivery)
    {
        if (id < 0)
            throw new ArgumentOutOfRangeException(nameof(id));
        if (customerId <= 0)
            throw new ArgumentException("Выберите клиента.", nameof(customerId));
        const DeliveryOptions allowed = DeliveryOptions.Courier | DeliveryOptions.Pickup | DeliveryOptions.Express;
        if ((delivery & ~allowed) != DeliveryOptions.None)
            throw new ArgumentOutOfRangeException(nameof(delivery), "Неизвестный вариант доставки.");

        return new Order
        {
            Id = id, CustomerId = customerId, OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Processing, Delivery = delivery
        };
    }

    public void AddItem(Product product, int quantity)
    {
        if (Id != 0)
            throw new InvalidOperationException("Нельзя изменять состав сохранённого заказа.");
        _items.Add(new OrderItem(product, quantity));
    }

    internal Order PrepareForCheckout(Customer customer, IReadOnlyDictionary<int, Product> products)
    {
        var savedOrder = Create(0, customer.Id, Delivery);
        savedOrder.Customer = customer;
        savedOrder.CustomerName = customer.FullName;
        foreach (var item in _items)
            savedOrder.AddItem(products[item.ProductId], item.Quantity);
        return savedOrder;
    }

    internal void CompleteCheckout(Order savedOrder)
    {
        Id = savedOrder.Id;
        OrderDate = savedOrder.OrderDate;
        Customer = savedOrder.Customer;
        CustomerName = savedOrder.CustomerName;
        _items.Clear();
        _items.AddRange(savedOrder._items);
    }

    internal void TransitionTo(OrderStatus next)
    {
        switch (Status, next)
        {
            case (OrderStatus.Processing, OrderStatus.Shipped):
                ShippedAt = LaterOf(DateTime.UtcNow, OrderDate);
                break;
            case (OrderStatus.Shipped, OrderStatus.Delivered):
                DeliveredAt = LaterOf(DateTime.UtcNow, ShippedAt!.Value);
                break;
            default:
                throw new InvalidOperationException($"Недопустимый переход статуса заказа: {Status} → {next}.");
        }
        Status = next;
    }

    private static DateTime LaterOf(DateTime a, DateTime b) => a > b ? a : b;
}
