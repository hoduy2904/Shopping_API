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
        public async void DeleteCategory(int id)
        {
            var category =await repository.GetAsync(id);
            repository.DeleteAsync(category);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
           var categories= await repository.GetAllAsync();
            return categories;
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public void InsertCategory(Category category)
        {
            repository.InsertAsync(category);

        }

        public void UpdateCategory(Category category)
        {
            repository.UpdateAsync(category);

        }
    }
}
