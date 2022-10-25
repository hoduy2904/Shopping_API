using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface ICartServices
    {
        Task<Cart> GetCartAsync(int id);
        Task<IEnumerable<Cart>> GetCartsAsync();
        void InsertCart(Cart cart);
        void UpdateCart(Cart cart);
        void DeleteCart(int id);
    }
}
