using System.Collections.Generic;
using System.Linq;
using ElectronicsStore.Entities;
using ElectronicsStore.Interfaces;

namespace ElectronicsStore.Repositories
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new List<Customer>();

        public IEnumerable<Customer> GetAll() => _customers;
        public Customer GetById(int id) => _customers.FirstOrDefault(c => c.Id == id);
        public void Add(Customer customer) => _customers.Add(customer);

        public void Update(Customer customer)
        {
            var index = _customers.FindIndex(c => c.Id == customer.Id);
            if (index >= 0) _customers[index] = customer;
        }

        public void Delete(int id) => _customers.RemoveAll(c => c.Id == id);
    }
}