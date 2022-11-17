using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<Role> GetRoleAsync(int id);
        IQueryable<Role> GetRoles();
        Task InsertRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(int id);
        IQueryable<Role> Where(Expression<Func<Role, bool>> expression);
        Task<Role> FindRoleByname(string roleName);
    }
}
