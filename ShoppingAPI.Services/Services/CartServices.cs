using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class CartServices : ICartServices
    {
        private readonly IRepository<Cart> repository;
        public CartServices(IRepository<Cart> repository)
        {
            this.repository = repository;
        }

        public async void DeleteCart(int id)
        {
           var cart=await repository.GetAsync(id);
           repository.DeleteAsync(cart);
        }

        public async Task<Cart> GetCartAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<Cart>> GetCartsAsync()
        {
           return await repository.GetAllAsync();
        }

        public void InsertCart(Cart cart)
        {
            repository.InsertAsync(cart);
        }

        public void UpdateCart(Cart cart)
        {
            repository.UpdateAsync(cart);
        }
    }
}
