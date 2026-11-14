using System;

namespace ElectronicsStore.Entities
{
    public class OrderItem
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product), "Товар не может быть null.");

            if (quantity <= 0)
                throw new ArgumentException("Количество должно быть больше нуля.", nameof(quantity));

            Quantity = quantity;
        }
    }
}