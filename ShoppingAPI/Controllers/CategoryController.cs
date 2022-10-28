﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [AllowAnonymous]
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
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> Category(int id)
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
                Status = (int)HttpStatusCode.NotFound,
                Success = false,
                Message = new[] { "Not found category" }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Category(Category category)
        {
            if (ModelState.IsValid)
            {
                await categoryServices.InsertCategory(category);
                return Ok(new ResultApi
                {
                    Status = 200,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = category
                });

            }
            return BadRequest();
        }

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
                    Status = 200,
                    Success = true,
                    Message = new[] { "Edit success" },
                    Data = CategoryDb
                });
            }
            return BadRequest();

        }

        [HttpDelete("Category")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await categoryServices.DeleteCategory(id);
            return Ok(new ResultApi
            {
                Status = 200,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
