using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Data;
using System.Net;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;
        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        [HttpGet,AllowAnonymous]
        public async Task<IActionResult> Products()
        {
            var products = await productServices.GetProductsAsync();
            return Ok(new ResultApi
            {
                Status = 200,
                Success = true,
                Data = products
            });
        }
        [HttpGet("{id}"),AllowAnonymous]
        public async Task<IActionResult> Product(int id)
        {
            try
            {
                var product = await productServices.GetProductAsync(id);
                if (product != null)
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Data = product,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Success = false,
                    Message = new[] { "Not found product" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Status = ex.HResult,
                    Success = false,
                    Message = new[] { ex.Message }
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Product(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await productServices.InsertProduct(product);
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Add Success" },
                        Data = product
                    });

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Status = ex.HResult,
                    Success = false,
                    Message = new[] { ex.Message },
                    Data = product
                });
            }
        }

        [HttpPut("Product")]
        public async Task<IActionResult> PutProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productDb = await productServices.GetProductAsync(product.Id);

                    productDb.CategoryId = product.CategoryId;
                    productDb.Name = product.Name;

                   await productServices.UpdateProduct(productDb);

                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Edit success" },
                        Data = productDb
                    });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Status = ex.HResult,
                    Success = false,
                    Message = new[] { ex.Message }
                });
            }
        }

        [HttpDelete("Product")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
               await productServices.DeleteProduct(id);
                return Ok(new ResultApi
                {
                    Status = 200,
                    Success = true,
                    Message = new[] { "Delete Success" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Status = ex.HResult,
                    Success = false,
                    Message = new[] { ex.Message }
                });
            }
        }
    }
}
