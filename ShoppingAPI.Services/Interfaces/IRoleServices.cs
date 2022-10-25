using ShoppingAPI.Data.Models;
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
        void InsertRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(int id);
    }
}
