using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class ShoppingDeliveryAddressServices : IShoppingDeliveryAddressServices
    {
        private readonly IRepository<ShoppingDeliveryAddress> repository;
        public ShoppingDeliveryAddressServices(IRepository<ShoppingDeliveryAddress> repository)
        {
            this.repository = repository;
        }
        public async Task DeleteShoppingDeliveryAddress(int id)
        {
            var infomationUser = await repository.GetAsync(id);
           await repository.DeleteAsync(infomationUser);
        }

        public async Task<ShoppingDeliveryAddress> GetShoppingDeliveryAddressAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<ShoppingDeliveryAddress>> GetShoppingDeliveryAddressesAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task InsertShoppingDeliveryAddress(ShoppingDeliveryAddress shoppingDeliveryAddress)
        {
            await repository.InsertAsync(shoppingDeliveryAddress);
        }

        public async Task UpdateShoppingDeliveryAddress(ShoppingDeliveryAddress shoppingDeliveryAddress)
        {
            await repository.UpdateAsync(shoppingDeliveryAddress);
        }
    }
}
