using System.Collections.Generic;
using System.Linq;
using ElectronicsStore.Entities;
using ElectronicsStore.Interfaces;

namespace ElectronicsStore.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> GetAll() => _products;
        public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
        public void Add(Product product) => _products.Add(product);

        public void Update(Product product)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index >= 0) _products[index] = product;
        }

        public void Delete(int id) => _products.RemoveAll(p => p.Id == id);
    }
}