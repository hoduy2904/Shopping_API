using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;
        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var products = await productServices.GetProductsAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = products
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Product(int id)
        {
            try
            {
                var product = await productServices.GetProductAsync(id);
                if (product != null)
                    return Ok(new ResultApi
                    {
                        Data = product,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Success = false,
                    Message = "Not found product"
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
        public IActionResult Product(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productServices.InsertProduct(product);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Add Success",
                        Data = product
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

                    productServices.UpdateProduct(productDb);

                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Edit success",
                        Data = productDb
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

        [HttpDelete("Product")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                productServices.DeleteProduct(id);
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
