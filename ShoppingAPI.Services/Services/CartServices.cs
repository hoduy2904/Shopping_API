using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteCartAsync(int id, int UserId)
        {
            var cart = await repository.Where(x => x.Id == id && x.UserId == UserId).SingleOrDefaultAsync();
            await repository.DeleteFromTrashAsync(cart);
        }

        public async Task<Cart> GetCartAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public IQueryable<Cart> GetCarts(int UserId)
        {
            return repository.Where(x => x.UserId == UserId && x.IsTrash == false);
        }

        public Cart GetCartByProduct(int ProductId, int ProductVariationId, int UserId)
        {
            var cart = repository.Where(x =>
            x.ProductId == ProductId
            && x.IsTrash == false
            && x.ProductVarationId == ProductVariationId
            && x.UserId == UserId)
                .Include(pv => pv.ProductVariation)
                .FirstOrDefault();
            return cart;
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
            await repository.DeleteFromTrashRangeAsync(carts);
        }

        public IQueryable<Cart> Where(Expression<Func<Cart, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
