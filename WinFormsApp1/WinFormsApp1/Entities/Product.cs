using System;
using ElectronicsStore.Enums;

namespace ElectronicsStore.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public ProductCategory Category { get; private set; }

        private Product(int id, string name, decimal price, int stockQuantity, ProductCategory category)
        {
            Id = id;
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            Category = category;
        }

        public static Product Create(int id, string name, decimal price, int stockQuantity, ProductCategory category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название товара не может быть пустым.", nameof(name));
            if (price < 0)
                throw new ArgumentException("Цена не может быть отрицательной.", nameof(price));
            if (stockQuantity < 0)
                throw new ArgumentException("Количество не может быть отрицательным.", nameof(stockQuantity));

            return new Product(id, name, price, stockQuantity, category);
        }
    }
}