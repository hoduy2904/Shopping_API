using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IUserRoleServices
    {
        Task<UserRole> GetUserRoleAsync(int id);
        Task<IEnumerable<UserRole>> GetUserRolesAsync();
        Task InsertUserRole(UserRole userRole);
        Task UpdateUserRole(UserRole userRole);
        Task DeleteUserRole(int id);
    }
}
