using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.Entities;

public class Customer
{
    [Key]
    public int Id { get; private set; }
    [Required, MaxLength(200)]
    public string FullName { get; private set; } = null!;
    [MaxLength(50)]
    public string Phone { get; private set; } = string.Empty;
    public bool IsArchived { get; private set; }

    private Customer() { }

    public static Customer Create(int id, string fullName, string phone)
    {
        fullName = fullName?.Trim() ?? string.Empty;
        phone = phone?.Trim() ?? string.Empty;
        if (id < 0)
            throw new ArgumentOutOfRangeException(nameof(id));
        if (fullName.Length is 0 or > 200)
            throw new ArgumentException("ФИО клиента должно содержать от 1 до 200 символов.", nameof(fullName));
        if (phone.Length > 50)
            throw new ArgumentException("Телефон не должен превышать 50 символов.", nameof(phone));

        return new Customer { Id = id, FullName = fullName, Phone = phone };
    }

    internal void ApplyChanges(Customer customer)
    {
        FullName = customer.FullName;
        Phone = customer.Phone;
    }

    internal void Archive() => IsArchived = true;
}
