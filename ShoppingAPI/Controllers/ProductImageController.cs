using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageServices productImageServices;
        public ProductImageController(IProductImageServices productImageServices)
        {
            this.productImageServices = productImageServices;
        }
        [HttpGet]
        public async Task<IActionResult> ProductImages()
        {
            var productImages = await productImageServices.GetProductImagesAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = productImages
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ProductImage(int id)
        {
            try
            {
                var productImage = await productImageServices.GetProductImageAsync(id);
                if (productImage != null)
                    return Ok(new ResultApi
                    {
                        Data = productImage,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Success = false,
                    Message = "Not found Product Image"
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
        public IActionResult ProductImage(ProductImage productImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productImageServices.InsertProductImage(productImage);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Add Success",
                        Data = productImage
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

                    productImageServices.UpdateProductImage(ProductImageDb);

                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Edit success",
                        Data = ProductImageDb
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

        [HttpDelete("ProductImage")]
        public IActionResult DeleteProductImage(int id)
        {
            try
            {
                productImageServices.DeleteProductImage(id);
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
