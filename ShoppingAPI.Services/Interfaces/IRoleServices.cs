﻿using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<Role> GetRoleAsync(int id);
        Task<IEnumerable<Role>> GetRolesAsync();
        Task InsertRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(int id);
    }
}