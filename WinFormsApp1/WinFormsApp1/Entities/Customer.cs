using System;

namespace ElectronicsStore.Entities
{
    public class Customer
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Phone { get; private set; }

        private Customer(int id, string fullName, string phone)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
        }

        public static Customer Create(int id, string fullName, string phone)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ФИО клиента не может быть пустым.", nameof(fullName));

            return new Customer(id, fullName, phone);
        }
    }
}