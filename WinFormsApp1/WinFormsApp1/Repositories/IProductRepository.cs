using System.Collections.Generic;
using ElectronicsStore.Entities;

namespace ElectronicsStore.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Update(Product product, int? expectedStockQuantity);
        void Delete(int id);
    }
}