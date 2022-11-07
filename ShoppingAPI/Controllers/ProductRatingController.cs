using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Security.Claims;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRatingController : ControllerBase
    {
        private readonly IProductRatingServices productRatingServices;
        private int UserId = -1;
        public ProductRatingController(IProductRatingServices productRatingServices, IHttpContextAccessor httpContextAccessor)
        {
            var ClaimUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            this.productRatingServices = productRatingServices;
            this.UserId = ClaimUserId == null ? -1 : int.Parse(ClaimUserId.Value);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ProductRating(int id)
        {
            var productRating = await productRatingServices.GetProductRatingAsync(id);
            return Ok(new ResultApi
            {
                Data = productRating,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpGet]
        public async Task<IActionResult> ProductRatings()
        {
            var productRatings = await productRatingServices.GetProductRatings();
            return Ok(new ResultApi
            {
                Data = productRatings,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpGet("{ProductId}")]
        public IActionResult ProductRatings(int ProductId)
        {
            var productRatings = productRatingServices.GetProductRatingsByProductid(ProductId);
            return Ok(new ResultApi
            {
                Data = productRatings,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpGet("{UserId}/{ProductId}/{ProductVariationId}")]
        [Authorize]
        public IActionResult ProductRatings(int UserId, int ProductId, int ProductVariationid)
        {
            if (this.UserId == UserId)
            {
                var productRatings = productRatingServices.GetProductRatings(UserId, ProductId, ProductVariationid);
                return Ok(new ResultApi
                {
                    Data = productRatings,
                    Status = Ok().StatusCode,
                    Success = true
                });
            }
            return Unauthorized();
        }

        [HttpGet("{ProductId}/{ProductVariationId}")]
        public IActionResult ProductRatings(int ProductId, int ProductVariationid)
        {
            var productRatings = productRatingServices.GetProductRatings(ProductId, ProductVariationid);
            return Ok(new ResultApi
            {
                Data = productRatings,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductRating(ProductRating productRating)
        {
            if (ModelState.IsValid)
            {
                productRating.UserId = this.UserId;

                await productRatingServices.InsertProductRatingAsync(productRating);
                return Ok(new ResultApi
                {
                    Message = new[] { "Insert Success Rating" },
                    Status = Ok().StatusCode,
                    Success = true
                });
            }
            return BadRequest();
        }

        [HttpPut("[Action]")]
        [Authorize]
        public async Task<IActionResult> PutProductRating(ProductRating productRating)
        {
            if (ModelState.IsValid)
            {
                var productRatingDb = await productRatingServices.GetProductRatingAsync(productRating.Id);

                //Check is User 
                if (productRatingDb.UserId != this.UserId)
                {
                    return Unauthorized();
                }
                //Check rating is Edit
                if (productRatingDb.isEdit)
                {
                    //If true then not accept edit
                    return BadRequest(new ResultApi
                    {
                        Message = new[] { "This Rating was Edited" },
                        Status = BadRequest().StatusCode,
                        Success = false
                    });
                }
                //if not
                //Update infomation rating
                productRatingDb.Description = productRating.Description;
                productRatingDb.isEdit = true;
                productRatingDb.Rating = productRating.Rating;

                await productRatingServices.UpdateProductRatingAsync(productRatingDb);

                return Ok(new ResultApi
                {
                    Message = new[] { "Update Success Rating" },
                    Status = Ok().StatusCode,
                    Success = true
                });
            }
            return BadRequest();
        }

        [HttpDelete("[Action]")]
        [Authorize]
        public async Task<IActionResult> DeleteProductRating(int id)
        {
            var productRating = await productRatingServices.GetProductRatingAsync(id);
            //Check is User of ProductRating
            //If not return unauthorized
            if (productRating.UserId != this.UserId)
                return Unauthorized();

            //If true then delete
            await productRatingServices.DeleteProductRatingAsync(id);
            return Ok(new ResultApi
            {
                Message = new[] { "Delete Success Rating" },
                Status = Ok().StatusCode,
                Success = true
            });
        }
    }
}
