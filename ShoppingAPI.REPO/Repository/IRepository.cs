using ShoppingAPI.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ShoppingAPI.REPO.Repository
{
    public interface IRepository<T> where T : BaseModels
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(int id);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> lstEntity);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> lstEntity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> lstEntity);
        Task DeleteFromTrashAsync(T entity);
        Task DeleteFromTrashRangeAsync(IEnumerable<T> lstEntity);
        Task SavechangesAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> preicate);
    }
}
