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

    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageServices productImageServices;
        public ProductImageController(IProductImageServices productImageServices)
        {
            this.productImageServices = productImageServices;
        }
        [HttpGet,AllowAnonymous]
        public async Task<IActionResult> ProductImages()
        {
            var productImages = await productImageServices.GetProductImagesAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = productImages
            });
        }
        [HttpGet("{id}"),AllowAnonymous]
        public async Task<IActionResult> ProductImage(int id)
        {
            try
            {
                var productImage = await productImageServices.GetProductImageAsync(id);
                if (productImage != null)
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Data = productImage,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Success = false,
                    Message = new[] { "Not found Product Image" }
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
        public async Task<IActionResult> ProductImage(ProductImage productImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await productImageServices.InsertProductImage(productImage);
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Add Success" },
                        Data = productImage
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
                    Data = productImage
                });
            }
        }

        [HttpPut("ProductImage")]
        public async Task<IActionResult> PutProductImage(ProductImage productImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ProductImageDb = await productImageServices.GetProductImageAsync(productImage.Id);

                    ProductImageDb.Image = productImage.Image;

                   await productImageServices.UpdateProductImage(ProductImageDb);

                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Edit success" },
                        Data = ProductImageDb
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

        [HttpDelete("ProductImage")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            try
            {
               await productImageServices.DeleteProductImage(id);
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
