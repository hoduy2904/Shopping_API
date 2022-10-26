using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IProductImageServices
    {
        Task<ProductImage> GetProductImageAsync(int id);
        Task<IEnumerable<ProductImage>> GetProductImagesAsync();
        Task InsertProductImage(ProductImage productImage);
        Task UpdateProductImage(ProductImage productImage);
        Task DeleteProductImage(int id);
    }
}
