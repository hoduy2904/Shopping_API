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
    internal class RoleServices : IRoleServices
    {
        private readonly IRepository<Role> repository;
        public RoleServices(IRepository<Role> repository)
        {
            this.repository = repository;
        }
        public async void DeleteRole(int id)
        {
            var role=await repository.GetAsync(id);
            repository.DeleteAsync(role);
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await repository.GetAllAsync();
        }

        public void InsertRole(Role role)
        {
            repository.InsertAsync(role);
        }

        public void UpdateRole(Role role)
        {
            repository.UpdateAsync(role);
        }
    }
}
