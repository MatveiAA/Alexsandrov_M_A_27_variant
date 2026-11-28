using System.Collections.Generic;
using ElectronicsStore.Entities;
using ElectronicsStore.Enums;

namespace ElectronicsStore.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Add(Order order);
        void UpdateStatus(int id, OrderStatus status);
    }
}