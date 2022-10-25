using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO;
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
    public class UserServices : IUserServices
    {
        private readonly IRepository<User> repository;
        public UserServices(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async void DeleteUser(int id)
        {
            var user = await repository.GetAsync(id);
            repository.DeleteAsync(user);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await repository.GetAllAsync();
        }

        public void InsertUser(User user)
        {
            repository.InsertAsync(user);
        }

        public void UpdateUser(User user)
        {
            repository.UpdateAsync(user);
        }
    }
}
