
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await categoryServices.GetCategoriesAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = categories
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Category(int id)
        {
            try
            {
                var category = await categoryServices.GetCategoryAsync(id);
                if (category != null)
                    return Ok(new ResultApi
                    {
                        Data = category,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Success = false,
                    Message = "Not found category"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult Category(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryServices.InsertCategory(category);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Add Success",
                        Data = category
                    });

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Success = false,
                    Message = ex.Message,
                    Data = category
                });
            }
        }

        [HttpPut("Category")]
        public async Task<IActionResult> PutCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var CategoryDb = await categoryServices.GetCategoryAsync(category.Id);

                    CategoryDb.Image = category.Image;
                    CategoryDb.Name = category.Name;
                    CategoryDb.CategoryId = category.CategoryId;

                    categoryServices.UpdateCategory(CategoryDb);

                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Edit success",
                        Data = CategoryDb
                    });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("Category")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                categoryServices.DeleteCategory(id);
                return Ok(new ResultApi
                {
                    Success = true,
                    Message = "Delete Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
