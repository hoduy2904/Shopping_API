using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IProductRatingImageServices
    {
        Task<ProductRatingImage> GetProductRatingImageAsync(int id);
        IQueryable<ProductRatingImage> GetProductRatingImagesByRatingId(int RatingId);
        Task<IEnumerable<ProductRatingImage>> GetProductRatingImages();
        Task InsertProductRatingImageAsync(ProductRatingImage productRating);
        Task InsertProductRatingImageRangeAsync(List<ProductRatingImage> productRatingImages);
        Task UpdateProductRatingImageAsync(ProductRatingImage productRating);
        Task DeleteProductRatingImageAsync(int id);
        Task UpdateProductRatingImageRangeAsync(List<ProductRatingImage> productRating);
        Task DeleteProductRatingImageRangeAsync(List<ProductRatingImage> productRatingImages);
        IQueryable<ProductRatingImage> Where(Expression<Func<ProductRatingImage, bool>> expression);
    }
}
