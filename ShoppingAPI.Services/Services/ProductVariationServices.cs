using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class ProductVariationServices : IProductVariationServices
    {
        private readonly IRepository<ProductVariation> repository;
        public ProductVariationServices(IRepository<ProductVariation> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteProductVariation(int id)
        {
            var productvariation = await repository.GetAsync(id);
           await repository.DeleteAsync(productvariation);
        }

        public async Task<IEnumerable<ProductVariation>> GetProductVariatiesAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<ProductVariation> GetProductVariationAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task InsertProductVariation(ProductVariation productVariation)
        {
           await repository.InsertAsync(productVariation);
        }

        public async Task UpdateProductVariation(ProductVariation productVariation)
        {
           await repository.UpdateAsync(productVariation);
        }
    }
}
