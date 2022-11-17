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
    public class ProductRatingImageServices : IProductRatingImageServices
    {
        private readonly IRepository<ProductRatingImage> repository;
        public ProductRatingImageServices(IRepository<ProductRatingImage> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteProductRatingImageAsync(int id)
        {
            var productRatingImage = await repository.GetAsync(id);
            await repository.DeleteAsync(productRatingImage);
        }

        public async Task DeleteProductRatingImageRangeAsync(List<ProductRatingImage> productRatingImages)
        {
            await repository.DeleteRangeAsync(productRatingImages);
        }

        public async Task<ProductRatingImage> GetProductRatingImageAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public IQueryable<ProductRatingImage> GetProductRatingImages()
        {
            return repository.GetAll();
        }

        public IQueryable<ProductRatingImage> GetProductRatingImagesByRatingId(int RatingId)
        {
            return repository.Where(x => x.IsTrash == false && x.ProductRatingId == RatingId);
        }

        public async Task InsertProductRatingImageAsync(ProductRatingImage productRating)
        {
            await repository.InsertAsync(productRating);
        }

        public async Task InsertProductRatingImageRangeAsync(List<ProductRatingImage> productRatingImages)
        {
            await repository.InsertRangeAsync(productRatingImages);
        }

        public async Task UpdateProductRatingImageAsync(ProductRatingImage productRating)
        {
            await repository.UpdateAsync(productRating);
        }

        public async Task UpdateProductRatingImageRangeAsync(List<ProductRatingImage> productRating)
        {
            await repository.UpdateRangeAsync(productRating);
        }

        public IQueryable<ProductRatingImage> Where(Expression<Func<ProductRatingImage, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
