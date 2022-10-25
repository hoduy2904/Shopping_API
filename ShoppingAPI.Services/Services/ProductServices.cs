using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> repository;
        public ProductServices(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async void DeleteProduct(int id)
        {
            var product = await repository.GetAsync(id);
            repository.DeleteAsync(product);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await repository.GetAllAsync();
        }

        public void InsertProduct(Product product)
        {
            repository.InsertAsync(product);
        }

        public void UpdateProduct(Product product)
        {
            repository.UpdateAsync(product);
        }
    }
}
