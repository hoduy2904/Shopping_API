using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IProductServices
    {
        Task<Product> GetProductAsync(int id);
        IQueryable<Product> GetProducts();
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        IQueryable<Product> Where(Expression<Func<Product, bool>> expression);
    }
}
