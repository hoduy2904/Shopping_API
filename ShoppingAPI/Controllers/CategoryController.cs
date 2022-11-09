
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
        private string folderRoot = Directory.GetCurrentDirectory() + "\\wwwroot";
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

            foreach (var item in categories)
            {
                item.Image = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + item.Image;
            }

            return Ok(new ResponseWithPaging
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = categories,
                PageCount = categories.PageCount,
                PageNumber = categories.PageNumber,
                TotalItems = categories.TotalItemCount
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
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Data = category,
                    Success = true
                });

            //if not exists return not found
            return NotFound(new ResponseApi
            {
                Status = (int)HttpStatusCode.NotFound,
                Success = false,
                Message = new[] { "Not found category" }
            });
        }

        //Insert category
        [HttpPost]
        public async Task<IActionResult> Category([FromForm] Category category, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    string PathImage = await SaveImage(imageFile);
                    category.Image = PathImage;
                }

                await categoryServices.InsertCategory(category);
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = category
                });

            }
            return BadRequest();
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            string extensionFile = Path.GetExtension(image.FileName);
            string newFileImage = $"{Guid.NewGuid()}{extensionFile}";
            string PathImage = SaveFileConfig.Image + newFileImage;
            string fullPath = Path.Combine(folderRoot + SaveFileConfig.Image, newFileImage);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return PathImage;
        }

        //Update category
        [HttpPut("Category")]
        public async Task<IActionResult> PutCategory([FromForm] Category category, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var CategoryDb = await categoryServices.GetCategoryAsync(category.Id);
                if (imageFile != null && imageFile.Length > 0)
                {
                    string PathImage = await SaveImage(imageFile);

                    CategoryDb.Image = PathImage;
                }
                CategoryDb.Name = category.Name;
                CategoryDb.CategoryId = category.CategoryId;

                await categoryServices.UpdateCategory(CategoryDb);

                return Ok(new ResponseApi
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
            return Ok(new ResponseApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
