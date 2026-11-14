using System.Collections.Generic;
using ElectronicsStore.Entities;

namespace ElectronicsStore.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        void Add(Order order);
    }
}