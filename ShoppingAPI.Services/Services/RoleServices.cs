﻿using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRepository<Role> repository;
        public RoleServices(IRepository<Role> repository)
        {
            this.repository = repository;
        }
        public async Task DeleteRole(int id)
        {
            var role=await repository.GetAsync(id);
           await repository.DeleteAsync(role);
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task InsertRole(Role role)
        {
           await repository.InsertAsync(role);
        }

        public async Task UpdateRole(Role role)
        {
           await repository.UpdateAsync(role);
        }
    }
}
