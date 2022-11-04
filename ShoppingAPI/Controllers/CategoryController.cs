
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Net;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        //Get all Categories
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await categoryServices.GetCategoriesAsync();
            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = categories
            });
        }

        //Get single category
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> Category(int id)
        {
            var category = await categoryServices.GetCategoryAsync(id);
            //Check exists category from id
            //have result to category
            if (category != null)
                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Data = category,
                    Success = true
                });

            //if not exists return not found
            return NotFound(new ResultApi
            {
                Status = (int)HttpStatusCode.NotFound,
                Success = false,
                Message = new[] { "Not found category" }
            });
        }

        //Insert category
        [HttpPost]
        public async Task<IActionResult> Category(Category category)
        {
            if (ModelState.IsValid)
            {
                await categoryServices.InsertCategory(category);
                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = category
                });

            }
            return BadRequest();
        }

        //Update category
        [HttpPut("Category")]
        public async Task<IActionResult> PutCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var CategoryDb = await categoryServices.GetCategoryAsync(category.Id);

                CategoryDb.Image = category.Image;
                CategoryDb.Name = category.Name;
                CategoryDb.CategoryId = category.CategoryId;

                await categoryServices.UpdateCategory(CategoryDb);

                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Edit success" },
                    Data = CategoryDb
                });
            }
            return BadRequest();

        }

        //Delete Category from Id
        [HttpDelete("Category")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await categoryServices.DeleteCategory(id);
            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
