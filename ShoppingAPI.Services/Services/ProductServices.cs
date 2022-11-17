using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace ShoppingAPI.Services.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> repository;
        public ProductServices(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await repository.GetAsync(id);
            await repository.DeleteAsync(product);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await repository.Where(x => x.Id == id && x.IsTrash == false)
                .Include(pi => pi.ProductImages)
                .Include(pv => pv.ProductVariations)
                .Include(pct => pct.Category)
                .Include(pr => pr.ProductRatings)
                .ThenInclude(pri => pri.productRatingImages)
                .SingleOrDefaultAsync();
        }

        public IQueryable<Product> GetProducts()
        {
            return repository.GetAll()
                .Include(pi => pi.ProductImages)
                .Include(pv => pv.ProductVariations)
                .Include(pct => pct.Category);

        }

        public async Task InsertProduct(Product product)
        {
            await repository.InsertAsync(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await repository.UpdateAsync(product);
        }
        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
