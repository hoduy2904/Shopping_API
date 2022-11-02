using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task DeleteCartAsync(int id)
        {
            var cart = await repository.GetAsync(id);
            await repository.DeleteAsync(cart);
        }

        public async Task<Cart> GetCartAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public IQueryable<Cart> GetCarts(int UserId)
        {
            return repository.Where(x => x.UserId == UserId && x.IsTrash == false);
        }

        public Cart GetCartByProduct(int ProductId, int ProductVariationId)
        {
            return repository.Where(x =>
            x.ProductId == ProductId
            && x.IsTrash == false
            && x.ProductVarationId == ProductVariationId)
                .FirstOrDefault();
        }

        public async Task InsertCartAsync(Cart cart)
        {
            await repository.InsertAsync(cart);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            await repository.UpdateAsync(cart);
        }

        public async Task InsertCartRangeAsync(IEnumerable<Cart> carts)
        {
            await repository.InsertRangeAsync(carts);
        }

        public async Task UpdateCartRangeAsync(IEnumerable<Cart> carts)
        {
            await repository.UpdateRangeAsync(carts);
        }

        public async Task DeleteCartRange(IEnumerable<Cart> carts)
        {
            await repository.DeleteRangeAsync(carts);
        }

        public IQueryable<Cart> Where(Expression<Func<Cart, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
