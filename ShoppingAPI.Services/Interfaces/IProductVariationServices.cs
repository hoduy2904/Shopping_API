using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IProductVariationServices
    {
        Task<ProductVariation> GetProductVariationAsync(int id);
        Task<IEnumerable<ProductVariation>> GetProductVariatiesAsync();
        Task InsertProductVariation(ProductVariation productVariation);
        Task UpdateProductVariation(ProductVariation productVariation);
        Task DeleteProductVariation(int id);
        IQueryable<ProductVariation> Where(Expression<Func<ProductVariation, bool>> expression);
    }
}
