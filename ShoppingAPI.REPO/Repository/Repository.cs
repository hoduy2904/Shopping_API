using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.REPO.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModels
    {
        private readonly ShoppingContext db;
        private readonly DbSet<T> entities;

        public Repository(ShoppingContext _db)
        {
            this.db = _db;
            this.entities = db.Set<T>();
        }


        public async Task DeleteAsync(T entity)
        {
            entity.IsTrash = true;
            entities.Update(entity);
            await db.SaveChangesAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            var entity = await entities.FindAsync(id);
            if (entity == null || entity.IsTrash == true)
                return null;
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.Where(x => x.IsTrash == false).ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            entity.Created = DateTime.Now;
            await entities.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task SavechangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entities.Update(entity);
            await db.SaveChangesAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> preicate)
        {
            return entities.Where(preicate);
        }
    }
}
