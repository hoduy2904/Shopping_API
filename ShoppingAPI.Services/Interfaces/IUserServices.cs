using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IUserServices
    {
        Task<User> GetUserAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task InsertUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<User> FindByUsername(string username);
        IQueryable<User> Where(Expression<Func<User, bool>> expression);
    }
}
