using System;
using Microsoft.Extensions.DependencyInjection;
using ElectronicsStore.Repositories;

namespace ElectronicsStore.Infrastructure
{
    public static class DependencyContainer
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            // Регистрируем зависимости (Интерфейс -> Заглушка)
            services.AddSingleton<IProductRepository, InMemoryProductRepository>();
            services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
            services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();

            // Позже здесь мы зарегистрируем наши формы

            return services.BuildServiceProvider();
        }
    }
}