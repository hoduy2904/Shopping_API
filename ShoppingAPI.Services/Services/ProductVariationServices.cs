using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System.Linq.Expressions;

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

        public IQueryable<ProductVariation> GetProductVariaties()
        {
            return repository.GetAll();
        }

        public async Task<ProductVariation> GetProductVariationAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<ProductVariationResponse> getProductVariationNumber(int ProductId, int ProductVariationId)
        {
            var productVRes = await repository.Where(x => x.IsTrash == false
            && x.ProductId == ProductId
            && x.Id == ProductVariationId)
                .Include(r => r.InvoicesDetails)
                .Select(r => new ProductVariationResponse
                {
                    ProductVariations = r.ProductVariations,
                    Numbers = r.Number - r.InvoicesDetails.Sum(x => x.Numbers),
                }).FirstOrDefaultAsync();
            return productVRes;
        }

        public async Task InsertProductVariation(ProductVariation productVariation)
        {
            await repository.InsertAsync(productVariation);
        }

        public async Task UpdateProductVariation(ProductVariation productVariation)
        {
            await repository.UpdateAsync(productVariation);
        }
        public IQueryable<ProductVariation> Where(Expression<Func<ProductVariation, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
