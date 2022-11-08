using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO;
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
    public class CategoryServices : ICategoryServices
    {
        private readonly IRepository<Category> repository;
        public CategoryServices(IRepository<Category> repository)
        {
            this.repository = repository;
        }
        public async Task DeleteCategory(int id)
        {
            var category = await repository.GetAsync(id);
            await repository.DeleteAsync(category);
        }

        public IQueryable<Category> GetCategories()
        {
            return repository.GetAll();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            var category = await repository
                .Where(x => x.Id == id && x.IsTrash == false)
                .Include(p => p.Products)
                .ThenInclude(pi => pi.ProductImages)
                .Include(p => p.Products)
                .ThenInclude(pi => pi.ProductVariations)
                .SingleOrDefaultAsync();
            return category;
        }

        public async Task InsertCategory(Category category)
        {
            await repository.InsertAsync(category);

        }

        public async Task UpdateCategory(Category category)
        {
            await repository.UpdateAsync(category);
        }
        public IQueryable<Category> Where(Expression<Func<Category, bool>> expression)
        {
            return repository.Where(expression);
        }
    }
}
