using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariationController : ControllerBase
    {
        private readonly IProductVariationServices productVariationServices;
        public ProductVariationController(IProductVariationServices productVariationServices)
        {
            this.productVariationServices = productVariationServices;
        }
        [HttpGet]
        public async Task<IActionResult> ProductVariations()
        {
            var productVariations = await productVariationServices.GetProductVariatiesAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = productVariations
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ProductVariation(int id)
        {
            try
            {
                var productVariation = await productVariationServices.GetProductVariationAsync(id);
                if (productVariation != null)
                    return Ok(new ResultApi
                    {
                        Data = productVariation,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Success = false,
                    Message = "Not found product Variation"
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
        public IActionResult ProductVariation(ProductVariation productVariation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productVariationServices.InsertProductVariation(productVariation);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Add Success",
                        Data = productVariation
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
                    Data = productVariation
                });
            }
        }

        [HttpPut("ProductVariation")]
        public async Task<IActionResult> PutProductVariation(ProductVariation productVariation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ProductVariationDb = await productVariationServices.GetProductVariationAsync(productVariation.Id);

                    ProductVariationDb.Number = productVariation.Number;
                    ProductVariationDb.PriceCurrent = productVariation.PriceCurrent;
                    ProductVariationDb.Name = productVariation.Name;
                    ProductVariationDb.PriceOld = productVariation.PriceOld;
                    ProductVariationDb.Variation = productVariation.Variation;

                    productVariationServices.UpdateProductVariation(ProductVariationDb);

                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Edit success",
                        Data = ProductVariationDb
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

        [HttpDelete("ProductVariation")]
        public IActionResult DeleteProductVariation(int id)
        {
            try
            {
                productVariationServices.DeleteProductVariation(id);
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
