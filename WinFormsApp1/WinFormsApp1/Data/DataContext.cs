using ElectronicsStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.Data;

public sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public const string ConnectionVariable = "ELECTRONICS_STORE_CONNECTION";

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public static string GetConnectionString()
    {
        var value = Environment.GetEnvironmentVariable(ConnectionVariable);
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"Задайте строку подключения PostgreSQL в переменной окружения {ConnectionVariable}.");
        return value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Product>(product =>
        {
            product.ToTable("Products", table =>
            {
                table.HasCheckConstraint("CK_Products_Price", "\"Price\" >= 0");
                table.HasCheckConstraint("CK_Products_StockQuantity", "\"StockQuantity\" >= 0");
            });
            product.HasIndex(p => new { p.IsArchived, p.Name });
            product.Property<uint>("Version").IsRowVersion();
        });

        model.Entity<Customer>(customer =>
        {
            customer.ToTable("Customers");
            customer.HasIndex(c => new { c.IsArchived, c.FullName });
            customer.Property<uint>("Version").IsRowVersion();
        });

        model.Entity<Order>(order =>
        {
            order.ToTable("Orders", table =>
            {
                table.HasCheckConstraint("CK_Orders_Delivery", "\"Delivery\" BETWEEN 0 AND 7");
                table.HasCheckConstraint("CK_Orders_StatusDates", """
                    ("Status" = 0 AND "ShippedAt" IS NULL AND "DeliveredAt" IS NULL)
                    OR ("Status" = 1 AND "ShippedAt" IS NOT NULL AND "ShippedAt" >= "OrderDate" AND "DeliveredAt" IS NULL)
                    OR ("Status" = 2 AND "ShippedAt" IS NOT NULL AND "ShippedAt" >= "OrderDate"
                        AND "DeliveredAt" IS NOT NULL AND "DeliveredAt" >= "ShippedAt")
                    """);
            });
            order.Property<uint>("Version").IsRowVersion();
            order.HasOne(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.Restrict);
            order.HasMany(o => o.Items).WithOne().HasForeignKey(i => i.OrderId).OnDelete(DeleteBehavior.Cascade);
            order.Navigation(o => o.Items).HasField("_items").UsePropertyAccessMode(PropertyAccessMode.Field);
            order.HasIndex(o => new { o.CustomerId, o.OrderDate });
            order.HasIndex(o => o.OrderDate);
            order.HasIndex(o => o.ShippedAt);
            order.HasIndex(o => o.DeliveredAt);
        });

        model.Entity<OrderItem>(item =>
        {
            item.ToTable("OrderItems", table =>
            {
                table.HasCheckConstraint("CK_OrderItems_Quantity", "\"Quantity\" > 0");
                table.HasCheckConstraint("CK_OrderItems_UnitPrice", "\"UnitPrice\" >= 0");
            });
            item.HasOne(i => i.Product).WithMany().HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
