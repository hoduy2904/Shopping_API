using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IUserRoleServices
    {
        Task<UserRole> GetUserRoleAsync(int id);
        IQueryable<UserRole> GetUserRoles();
        Task InsertUserRole(UserRole userRole);
        Task UpdateUserRole(UserRole userRole);
        Task DeleteUserRole(int id);
        IQueryable<UserRole> Where(Expression<Func<UserRole, bool>> expression);
    }
}
