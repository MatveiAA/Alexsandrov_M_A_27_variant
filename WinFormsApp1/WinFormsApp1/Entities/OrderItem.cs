using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicsStore.Enums;

namespace ElectronicsStore.Entities;

public class OrderItem
{
    [Key]
    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; private set; } = null!;
    public int Quantity { get; private set; }
    [Required, MaxLength(200)]
    public string ProductName { get; private set; } = null!;
    public ProductCategory Category { get; private set; }
    [Column(TypeName = "numeric(18,2)")]
    public decimal UnitPrice { get; private set; }
    [NotMapped]
    public decimal LineTotal => UnitPrice * Quantity;

    private OrderItem() { }

    public OrderItem(Product product, int quantity)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (product.Id <= 0)
            throw new ArgumentException("Выберите сохранённый товар.", nameof(product));
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть больше нуля.", nameof(quantity));

        Product = product;
        ProductId = product.Id;
        Quantity = quantity;
        CaptureSnapshot(product);
    }

    private void CaptureSnapshot(Product product)
    {
        ProductName = product.Name;
        Category = product.Category;
        UnitPrice = product.Price;
    }

}
