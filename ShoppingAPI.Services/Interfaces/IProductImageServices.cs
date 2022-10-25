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
        void InsertProductImage(ProductImage productImage);
        void UpdateProductImage(ProductImage productImage);
        void DeleteProductImage(int id);
    }
}
