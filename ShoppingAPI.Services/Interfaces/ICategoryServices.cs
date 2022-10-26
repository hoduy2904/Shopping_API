using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<Category> GetCategoryAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task InsertCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}
