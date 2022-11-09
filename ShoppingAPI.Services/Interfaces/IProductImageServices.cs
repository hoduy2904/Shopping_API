using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IProductImageServices
    {
        Task<ProductImage> GetProductImageAsync(int id);
        IQueryable<ProductImage> GetProductImages();
        Task InsertProductImage(ProductImage productImage);
        Task InsertProductImages(IEnumerable<ProductImage> productImages);
        Task UpdateProductImage(ProductImage productImage);
        Task DeleteProductImage(int id);
        IQueryable<ProductImage> Where(Expression<Func<ProductImage, bool>> expression);
    }
}
