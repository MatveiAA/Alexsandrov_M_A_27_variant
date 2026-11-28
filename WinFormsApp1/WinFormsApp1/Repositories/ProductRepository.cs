using ElectronicsStore.Data;
using ElectronicsStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ElectronicsStore.Repositories;

public sealed class ProductRepository(IDbContextFactory<DataContext> factory, ILogger<ProductRepository> logger)
    : IProductRepository
{
    public IEnumerable<Product> GetAll() => Execute(nameof(GetAll), null, db =>
    {
        var products = db.Products.AsNoTracking().Where(p => !p.IsArchived)
            .OrderBy(p => p.Name).ThenBy(p => p.Id).ToList();
        logger.LogDebug("Получено товаров: {Count}", products.Count);
        return products;
    });

    public Product? GetById(int id) => Execute(nameof(GetById), id, db =>
    {
        var product = db.Products.AsNoTracking().SingleOrDefault(p => p.Id == id && !p.IsArchived);
        logger.LogDebug("Товар {ProductId} найден: {Found}", id, product is not null);
        return product;
    });

    public void Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        Execute(nameof(Add), product.Id, db =>
        {
            if (product.Id != 0)
                throw new InvalidOperationException("Идентификатор нового товара должен быть равен нулю.");
            db.Products.Add(product);
            var count = db.SaveChanges();
            logger.LogInformation("Создан товар {ProductId}, категория {Category}, цена {Price}, остаток {Stock}",
                product.Id, product.Category, product.Price, product.StockQuantity);
            return count;
        });
    }

    public void Update(Product product) => Update(product, expectedStockQuantity: null);

    public void Update(Product product, int? expectedStockQuantity)
    {
        ArgumentNullException.ThrowIfNull(product);
        Execute(nameof(Update), product.Id, db =>
        {
            var current = FindActive(db, product.Id);
            if (expectedStockQuantity is int stock && stock != current.StockQuantity)
                throw new InvalidOperationException("Остаток товара уже изменился. Обновите данные и повторите операцию.");
            current.ApplyChanges(product);
            var count = db.SaveChanges();
            logger.LogInformation("Обновлён товар {ProductId}, категория {Category}, цена {Price}, остаток {Stock}",
                product.Id, product.Category, product.Price, product.StockQuantity);
            return count;
        });
    }

    public void Delete(int id) => Execute(nameof(Delete), id, db =>
    {
        FindActive(db, id).Archive();
        var count = db.SaveChanges();
        logger.LogInformation("Товар {ProductId} перемещён в архив", id);
        return count;
    });

    private static Product FindActive(DataContext db, int id) =>
        db.Products.SingleOrDefault(p => p.Id == id && !p.IsArchived)
        ?? throw new InvalidOperationException($"Товар №{id} не найден или находится в архиве.");

    private T Execute<T>(string operation, int? id, Func<DataContext, T> action) =>
        RepositoryOperation.Run(factory, logger, $"Product.{operation}", id, action);
}
