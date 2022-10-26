using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IProductServices
    {
        Task<Product> GetProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
