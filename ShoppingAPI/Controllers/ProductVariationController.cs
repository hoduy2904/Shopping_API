using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Common.Models;
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
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ProductVariations()
        {
            var productVariations = await productVariationServices.GetProductVariatiesAsync();
            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = productVariations
            });
        }
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> ProductVariation(int id)
        {
            var productVariation = await productVariationServices.GetProductVariationAsync(id);
            if (productVariation != null)
                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
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

        [HttpPost]
        public async Task<IActionResult> ProductVariation(ProductVariation productVariation)
        {
            if (ModelState.IsValid)
            {
                await productVariationServices.InsertProductVariation(productVariation);
                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = productVariation
                });

            }
            return BadRequest();
        }

        [HttpPut("ProductVariation")]
        public async Task<IActionResult> PutProductVariation(ProductVariation productVariation)
        {
            if (ModelState.IsValid)
            {
                var ProductVariationDb = await productVariationServices.GetProductVariationAsync(productVariation.Id);

                ProductVariationDb.Number = productVariation.Number;
                ProductVariationDb.PriceCurrent = productVariation.PriceCurrent;
                ProductVariationDb.Name = productVariation.Name;
                ProductVariationDb.PriceOld = productVariation.PriceOld;
                ProductVariationDb.VariationId = productVariation.VariationId;

                await productVariationServices.UpdateProductVariation(ProductVariationDb);

                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Edit success" },
                    Data = ProductVariationDb
                });
            }
            return BadRequest();
        }

        [HttpDelete("ProductVariation")]
        public async Task<IActionResult> DeleteProductVariation(int id)
        {
            await productVariationServices.DeleteProductVariation(id);
            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
