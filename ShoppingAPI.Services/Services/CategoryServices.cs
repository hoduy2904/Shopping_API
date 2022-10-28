using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var category =await repository.GetAsync(id);
           await repository.DeleteAsync(category);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
           return await repository.GetAllAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task InsertCategory(Category category)
        {
            await repository.InsertAsync(category);

        }

        public async Task UpdateCategory(Category category)
        {
            await repository.UpdateAsync(category);

        }
    }
}
