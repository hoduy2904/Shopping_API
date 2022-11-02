using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface ICartServices
    {
        Task<Cart> GetCartAsync(int id);
        IQueryable<Cart> GetCarts(int UserId);
        Task InsertCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int id);
        Cart GetCartByProduct(int ProductId, int ProductVariationId);
        IQueryable<Cart> Where(Expression<Func<Cart, bool>> expression);
    }
}
