using System;
using System.Collections.Generic;
using ElectronicsStore.Enums;

namespace ElectronicsStore.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime OrderDate { get; private set; }
        public int CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public DeliveryOptions Delivery { get; private set; }

        private readonly List<OrderItem> _items = new List<OrderItem>();
        public IReadOnlyList<OrderItem> Items => _items;

        private Order(int id, int customerId, DeliveryOptions delivery)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = DateTime.Now;
            Status = OrderStatus.Processing;
            Delivery = delivery;
        }

        public static Order Create(int id, int customerId, DeliveryOptions delivery)
        {
            if (customerId <= 0)
                throw new ArgumentException("Id клиента должен быть больше нуля.", nameof(customerId));

            return new Order(id, customerId, delivery);
        }

        public void AddItem(Product product, int quantity)
        {
            var newItem = new OrderItem(product, quantity);
            _items.Add(newItem);
        }
    }
}