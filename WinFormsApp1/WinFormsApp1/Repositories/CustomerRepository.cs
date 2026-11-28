using ElectronicsStore.Data;
using ElectronicsStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ElectronicsStore.Repositories;

public sealed class CustomerRepository(IDbContextFactory<DataContext> factory, ILogger<CustomerRepository> logger)
    : ICustomerRepository
{
    public IEnumerable<Customer> GetAll() => Execute(nameof(GetAll), null, db =>
    {
        var customers = db.Customers.AsNoTracking().Where(c => !c.IsArchived)
            .OrderBy(c => c.FullName).ThenBy(c => c.Id).ToList();
        logger.LogDebug("Получено клиентов: {Count}", customers.Count);
        return customers;
    });

    public Customer? GetById(int id) => Execute(nameof(GetById), id, db =>
    {
        var customer = db.Customers.AsNoTracking().SingleOrDefault(c => c.Id == id && !c.IsArchived);
        logger.LogDebug("Клиент {CustomerId} найден: {Found}", id, customer is not null);
        return customer;
    });

    public void Add(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        Execute(nameof(Add), customer.Id, db =>
        {
            if (customer.Id != 0)
                throw new InvalidOperationException("Идентификатор нового клиента должен быть равен нулю.");
            db.Customers.Add(customer);
            var count = db.SaveChanges();
            logger.LogInformation("Создан клиент {CustomerId}", customer.Id);
            return count;
        });
    }

    public void Update(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        Execute(nameof(Update), customer.Id, db =>
        {
            FindActive(db, customer.Id).ApplyChanges(customer);
            var count = db.SaveChanges();
            logger.LogInformation("Обновлён клиент {CustomerId}", customer.Id);
            return count;
        });
    }

    public void Delete(int id) => Execute(nameof(Delete), id, db =>
    {
        FindActive(db, id).Archive();
        var count = db.SaveChanges();
        logger.LogInformation("Клиент {CustomerId} перемещён в архив", id);
        return count;
    });

    private static Customer FindActive(DataContext db, int id) =>
        db.Customers.SingleOrDefault(c => c.Id == id && !c.IsArchived)
        ?? throw new InvalidOperationException($"Клиент №{id} не найден или находится в архиве.");

    private T Execute<T>(string operation, int? id, Func<DataContext, T> action) =>
        RepositoryOperation.Run(factory, logger, $"Customer.{operation}", id, action);
}
