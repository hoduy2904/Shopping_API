using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IProductRatingServices
    {
        Task<ProductRating> GetProductRatingAsync(int id);
        IQueryable<ProductRating> GetProductRatingsByUserId(int UserId);
        IQueryable<ProductRating> GetProductRatings(int UserId,int ProductId,int ProductVariationId);
        IQueryable<ProductRating> GetProductRatings(int ProductId, int ProductVariationId);
        IQueryable<ProductRating> GetProductRatingsByProductid(int ProductId);
        Task<IEnumerable<ProductRating>> GetProductRatings();
        Task InsertProductRatingAsync(ProductRating productRating);
        Task UpdateProductRatingAsync(ProductRating productRating);
        Task DeleteProductRatingAsync(int id);
        IQueryable<ProductRating> Where(Expression<Func<ProductRating, bool>> expression);
    }
}
