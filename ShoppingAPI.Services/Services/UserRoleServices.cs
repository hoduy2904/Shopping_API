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
    public class UserRoleServices : IUserRoleServices
    {
        private readonly IRepository<UserRole> repository;
        public UserRoleServices(IRepository<UserRole> repository)
        {
            this.repository = repository;
        }
        public async Task DeleteUserRole(int id)
        {
            var userRole = await repository.GetAsync(id);
            await repository.DeleteAsync(userRole);
        }

        public async Task<UserRole> GetUserRoleAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task InsertUserRole(UserRole userRole)
        {
           await repository.InsertAsync(userRole);
        }

        public async Task UpdateUserRole(UserRole userRole)
        {
           await repository.UpdateAsync(userRole);
        }
    }
}