using System.Collections.Generic;
using ElectronicsStore.Entities;
using ElectronicsStore.Interfaces;

namespace ElectronicsStore.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public IEnumerable<Order> GetAll() => _orders;
        public void Add(Order order) => _orders.Add(order);
    }
}