using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class ProductImageServices : IProductImageServices
    {
        private readonly IRepository<ProductImage> repository;
        public ProductImageServices(IRepository<ProductImage> repository)
        {
            this.repository = repository;
        }

        public async void DeleteProductImage(int id)
        {
            var productImage =await repository.GetAsync(id);
            repository.DeleteAsync(productImage);
        }

        public async Task<ProductImage> GetProductImageAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync()
        {
            return await repository.GetAllAsync();
        }

        public void InsertProductImage(ProductImage productImage)
        {
            repository.InsertAsync(productImage);
        }

        public void UpdateProductImage(ProductImage productImage)
        {
            repository.UpdateAsync(productImage);
        }
    }
}
