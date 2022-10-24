using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Models;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ShoppingContext shoppingContext;
        public CategoryController(ShoppingContext shoppingContext)
        {
            this.shoppingContext = shoppingContext;
        }
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await shoppingContext.Categories.ToListAsync();
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
                var category = await shoppingContext.Categories.FindAsync(id);
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
        public async Task<IActionResult> Category(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.Created = DateTime.Now;
                    await shoppingContext.Categories.AddAsync(category);

                    await shoppingContext.SaveChangesAsync();
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
                    var CategoryDb = await shoppingContext.Categories.FindAsync(category.Id);

                    CategoryDb.Image = category.Image;
                    CategoryDb.Name = category.Name;
                    CategoryDb.CategoryId = category.CategoryId;

                    shoppingContext.Categories.Update(CategoryDb);

                    await shoppingContext.SaveChangesAsync();

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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category=await shoppingContext.Categories.FindAsync(id);
                shoppingContext.Categories.Remove(category);

                await shoppingContext.SaveChangesAsync();
                return Ok(new ResultApi
                {
                    Success=true,
                    Data = category,
                    Message = "Delete Success"
                });
            }catch(Exception ex)
            {
                return BadRequest(new ResultApi
                {
                   Success=false,
                   Message=ex.Message
                });
            }
        }
    }
}
