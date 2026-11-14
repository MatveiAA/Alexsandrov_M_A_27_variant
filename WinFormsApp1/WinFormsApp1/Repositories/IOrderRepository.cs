using System.Collections.Generic;
using ElectronicsStore.Entities;

namespace ElectronicsStore.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        void Add(Order order);
    }
}