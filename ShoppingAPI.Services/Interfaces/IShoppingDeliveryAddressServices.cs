using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IShoppingDeliveryAddressServices
    {
        Task<ShoppingDeliveryAddress> GetShoppingDeliveryAddressAsync(int id);
        Task<IEnumerable<ShoppingDeliveryAddress>> GetShoppingDeliveryAddressesAsync();
        Task InsertShoppingDeliveryAddress(ShoppingDeliveryAddress shoppingDeliveryAddress);
        Task UpdateShoppingDeliveryAddress(ShoppingDeliveryAddress shoppingDeliveryAddress);
        Task DeleteShoppingDeliveryAddress(int id);
        IQueryable<ShoppingDeliveryAddress> Where(Expression<Func<ShoppingDeliveryAddress, bool>> expression);
    }
}
