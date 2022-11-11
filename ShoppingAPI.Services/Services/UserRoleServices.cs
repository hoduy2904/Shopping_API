using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            await repository.DeleteFromTrashAsync(userRole);
        }

        public async Task<UserRole> GetUserRoleAsync(int id)
        {
            return await repository
                .Where(ur => ur.Id == id && ur.IsTrash == false)
                .Include(u => u.User)
                .Include(r => r.Role)
                .SingleOrDefaultAsync();
        }

        public IQueryable<UserRole> GetUserRoles()
        {
            return repository.GetAll();
        }

        public async Task InsertUserRole(UserRole userRole)
        {
            await repository.InsertAsync(userRole);
        }

        public async Task UpdateUserRole(UserRole userRole)
        {
            await repository.UpdateAsync(userRole);
        }

        public IQueryable<UserRole> Where(Expression<Func<UserRole, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
