using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class ProductRatingServices : IProductRatingServices
    {
        private readonly IRepository<ProductRating> repository;
        public ProductRatingServices(IRepository<ProductRating> repository)
        {
            this.repository = repository;
        }
        public async Task DeleteProductRatingAsync(int id)
        {
            var productRating = await repository.GetAsync(id);
            await repository.DeleteAsync(productRating);
        }

        public async Task<ProductRating> GetProductRatingAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public IQueryable<ProductRating> GetProductRatings(int UserId, int ProductId, int ProductVariationId)
        {
            return repository.Where(
                x => x.UserId == UserId
            && x.ProductId == ProductId
            && x.ProductVariationId == ProductVariationId
            && x.IsTrash == false)
                .Include(pri => pri.productRatingImages)
                .Include(p => p.Product)
                .Include(pv => pv.ProductVariation);
        }

        public IQueryable<ProductRating> GetProductRatings(int ProductId, int ProductVariationId)
        {
            return repository.Where(
                x => x.ProductId == ProductId
            && x.ProductVariationId == ProductVariationId
            && x.IsTrash == false)
                .Include(pri => pri.productRatingImages)
                .Include(p => p.Product)
                .Include(pv => pv.ProductVariation);
        }

        public IQueryable<ProductRating> GetProductRatings()
        {
            return repository.GetAll();
        }

        public IQueryable<ProductRating> GetProductRatingsByProductid(int ProductId)
        {
            return repository.Where(x => x.ProductId == ProductId && x.IsTrash == false);
        }

        public IQueryable<ProductRating> GetProductRatingsByUserId(int UserId)
        {
            return repository.Where(x => x.UserId == UserId && x.IsTrash == false);
        }

        public async Task InsertProductRatingAsync(ProductRating productRating)
        {
            await repository.InsertAsync(productRating);
        }

        public async Task UpdateProductRatingAsync(ProductRating productRating)
        {
            await repository.UpdateAsync(productRating);
        }

        public IQueryable<ProductRating> Where(Expression<Func<ProductRating, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
