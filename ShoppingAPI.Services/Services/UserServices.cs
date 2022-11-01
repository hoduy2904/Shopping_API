using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<User> repository;
        public UserServices(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteUser(int id)
        {
            var user = await repository.GetAsync(id);
            await repository.DeleteAsync(user);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await repository
                .Where(x => x.IsTrash == false && x.Id == id)
                .Include(sd => sd.ShoppingDeliveryAddresses)
                .Include(ur => ur.UserRoles)
                .ThenInclude(r=>r.Role)
                .Select(x => new User
                {
                    Sex= x.Sex,
                    Email=x.Email,
                    Created = x.Created,
                    IdentityCard = x.IdentityCard,
                    UserRoles=x.UserRoles,
                    Username = x.Username,
                    FristName = x.FristName,
                    LastName = x.LastName,
                    ShoppingDeliveryAddresses=x.ShoppingDeliveryAddresses,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<User> FindByUsername(string username)
        {
            return await repository.Where(x => x.Username.Equals(username)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            // return repository.Where(x => x.IsTrash == false).ToList();
            return await repository.GetAllAsync();
        }

        public async Task InsertUser(User user)
        {
            await repository.InsertAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            await repository.UpdateAsync(user);
        }

        public IQueryable<User> Where(Expression<Func<User, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
