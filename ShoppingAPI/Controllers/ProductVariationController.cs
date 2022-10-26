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

    public class ProductVariationController : ControllerBase
    {
        private readonly IProductVariationServices productVariationServices;
        public ProductVariationController(IProductVariationServices productVariationServices)
        {
            this.productVariationServices = productVariationServices;
        }
        [HttpGet,AllowAnonymous]
        public async Task<IActionResult> ProductVariations()
        {
            var productVariations = await productVariationServices.GetProductVariatiesAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = productVariations
            });
        }
        [HttpGet("{id}"),AllowAnonymous]
        public async Task<IActionResult> ProductVariation(int id)
        {
            try
            {
                var productVariation = await productVariationServices.GetProductVariationAsync(id);
                if (productVariation != null)
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Data = productVariation,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Success = false,
                    Message = new[] { "Not found product Variation" }
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
        public async Task<IActionResult> ProductVariation(ProductVariation productVariation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await productVariationServices.InsertProductVariation(productVariation);
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Add Success" },
                        Data = productVariation
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

                    await productVariationServices.UpdateProductVariation(ProductVariationDb);

                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Edit success" },
                        Data = ProductVariationDb
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

        [HttpDelete("ProductVariation")]
        public async Task<IActionResult> DeleteProductVariation(int id)
        {
            try
            {
                await productVariationServices.DeleteProductVariation(id);
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
