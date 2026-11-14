using System;
using Microsoft.Extensions.DependencyInjection;
using ElectronicsStore.Repositories;
using ElectronicsStore.Interfaces;

namespace ElectronicsStore.Infrastructure
{
    public static class DependencyContainer
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IProductRepository, InMemoryProductRepository>();
            services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
            services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();

            return services.BuildServiceProvider();
        }
    }
}