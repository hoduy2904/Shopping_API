using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IProductVariationServices
    {
        Task<ProductVariation> GetProductVariationAsync(int id);
        Task<IEnumerable<ProductVariation>> GetProductVariatiesAsync();
        void InsertProductVariation(ProductVariation productVariation);
        void UpdateProductVariation(ProductVariation productVariation);
        void DeleteProductVariation(int id);
    }
}
