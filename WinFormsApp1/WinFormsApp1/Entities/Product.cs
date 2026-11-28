using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicsStore.Enums;

namespace ElectronicsStore.Entities;

public class Product
{
    [Key]
    public int Id { get; private set; }
    [Required, MaxLength(200)]
    public string Name { get; private set; } = null!;
    [Column(TypeName = "numeric(18,2)")]
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public ProductCategory Category { get; private set; }
    [MaxLength(4000)]
    public string Specifications { get; private set; } = string.Empty;
    public bool IsArchived { get; private set; }

    private Product() { }

    public static Product Create(int id, string name, decimal price, int stockQuantity,
        ProductCategory category, string specifications = "")
    {
        name = name?.Trim() ?? string.Empty;
        specifications = specifications?.Trim() ?? string.Empty;
        if (id < 0)
            throw new ArgumentOutOfRangeException(nameof(id));
        if (name.Length is 0 or > 200)
            throw new ArgumentException("Название товара должно содержать от 1 до 200 символов.", nameof(name));
        if (price < 0 || price > 9999999999999999.99m || decimal.Round(price, 2) != price)
            throw new ArgumentException("Укажите неотрицательную цену не более чем с двумя знаками после запятой.", nameof(price));
        if (stockQuantity < 0)
            throw new ArgumentException("Количество не может быть отрицательным.", nameof(stockQuantity));
        if (!Enum.IsDefined(category))
            throw new ArgumentOutOfRangeException(nameof(category), "Неизвестная категория товара.");
        if (specifications.Length > 4000)
            throw new ArgumentException("Характеристики не должны превышать 4000 символов.", nameof(specifications));

        return new Product
        {
            Id = id, Name = name, Price = price, StockQuantity = stockQuantity,
            Category = category, Specifications = specifications
        };
    }

    internal void ApplyChanges(Product product)
    {
        Name = product.Name;
        Price = product.Price;
        StockQuantity = product.StockQuantity;
        Category = product.Category;
        Specifications = product.Specifications;
    }

    internal void RemoveFromStock(int quantity)
    {
        if (quantity <= 0 || quantity > StockQuantity)
            throw new InvalidOperationException($"Недостаточно товара «{Name}» на складе: доступно {StockQuantity}, запрошено {quantity}.");
        StockQuantity -= quantity;
    }

    internal void Archive() => IsArchived = true;
}
