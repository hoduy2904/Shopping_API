
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Extensions;
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

        //Get all Categories with Paging
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Categories(int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var categories = await categoryServices
                .GetCategories().OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = new ResultWithPaging
                {
                    Data = categories,
                    PageCount = categories.PageCount,
                    PageNumber = categories.PageNumber,
                    TotalItems = categories.TotalItemCount
                }
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
