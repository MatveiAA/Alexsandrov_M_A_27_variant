using ElectronicsStore.Data;
using ElectronicsStore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ElectronicsStore.Infrastructure;

public static class DependencyContainer
{
    public static ServiceProvider Configure(Serilog.ILogger logger)
    {
        var services = new ServiceCollection();
        services.AddLogging(builder => builder.ClearProviders().AddSerilog(logger));
        services.AddDbContextFactory<DataContext>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<MainForm>();
        services.AddTransient<ProductListForm>();
        services.AddTransient<CustomerListForm>();
        services.AddTransient<OrderListForm>();
        return services.BuildServiceProvider(validateScopes: true);
    }
}
