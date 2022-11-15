using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Extensions;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Model;
using ShoppingAPI.Services.Interfaces;
using System.Security.Claims;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRatingController : ControllerBase
    {
        private readonly IProductRatingServices productRatingServices;
        private readonly IInvoiceDetailsServices invoiceDetailsServices;
        private int UserId = -1;
        public ProductRatingController(IProductRatingServices productRatingServices,
            IHttpContextAccessor httpContextAccessor,
            IInvoiceDetailsServices invoiceDetailsServices
            )
        {
            var ClaimUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            this.productRatingServices = productRatingServices;
            this.UserId = ClaimUserId == null ? -1 : int.Parse(ClaimUserId.Value);
            this.invoiceDetailsServices = invoiceDetailsServices;
        }

        //Get one rating product from id
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> getProductRating(int id)
        {
            var productRating = await productRatingServices.GetProductRatingAsync(id);
            return Ok(new ResponseApi
            {
                Data = productRating,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        //Get all product ratings

        [HttpGet("[Action]")]
        public async Task<IActionResult> getProductRatings(int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var productRatings = await productRatingServices
                .GetProductRatings()
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Data = productRatings,
                PageCount = productRatings.PageCount,
                PageNumber = productRatings.PageNumber,
                TotalItems = productRatings.TotalItemCount,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        //Get all product rating from productId
        [HttpGet("[Action]/{ProductId}")]
        public async Task<IActionResult> getProductRatingsByPid(int ProductId, int? page, int? pageSize)
        {
            //If page is null then get page Default
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            //If pageSize is null then get pageSize Default

            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            //Get product ratings and sort by Id Paging
            var productRatings = await productRatingServices
                .GetProductRatingsByProductid(ProductId)
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Data = productRatings,
                PageCount = productRatings.PageCount,
                PageNumber = productRatings.PageNumber,
                TotalItems = productRatings.TotalItemCount,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        //Get ratings from userid , product id and product variationid
        [HttpGet("[Action]/{UserId}/{ProductId}/{ProductVariationId}")]
        [Authorize]
        public async Task<IActionResult> getProductRatingsByUidPidPvId(int UserId, int ProductId, int ProductVariationid, int? page, int? pageSize)
        {
            if (this.UserId == UserId)
            {
                if (page == null)
                    page = PagingSettingsConfig.pageDefault;
                if (pageSize == null)
                    pageSize = PagingSettingsConfig.pageSize;

                
                var productRatings = await productRatingServices
                    .GetProductRatings(UserId, ProductId, ProductVariationid)
                    .OrderByDescending(x => x.Id)
                    .ToPagedList(page.Value, pageSize.Value);

                return Ok(new ResponseWithPaging
                {
                    Data = productRatings,
                    PageCount = productRatings.PageCount,
                    PageNumber = productRatings.PageNumber,
                    TotalItems = productRatings.TotalItemCount,
                    Status = Ok().StatusCode,
                    Success = true
                });
            }
            return Unauthorized();
        }

        //Get ratings from  product id and product variationid

        [HttpGet("[Action]/{ProductId}/{ProductVariationId}")]
        public async Task<IActionResult> getProductRatingsByPidPvId(int ProductId, int ProductVariationid, int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var productRatings = await productRatingServices
                .GetProductRatings(ProductId, ProductVariationid)
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Data = productRatings,
                PageCount = productRatings.PageCount,
                PageNumber = productRatings.PageNumber,
                TotalItems = productRatings.TotalItemCount,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        //POST Product rating
        [HttpPost("[Action]")]
        [Authorize]
        public async Task<IActionResult> insertProductRating(ProductRatingModel productRatingModel)
        {
            if (ModelState.IsValid)
            {
                //GET Invoice details valid (every invoice from product then only reviewed 1 time ) (Check user bought product)
                var invoiceDetails = invoiceDetailsServices
                    .Where(x => x.ProductId == productRatingModel.ProductId
                && x.ProductVariationId == productRatingModel.ProductVariationId)
                    .Select(iv => iv.Invoice)
                    .Where(u => u.UserId == this.UserId)
                    .FirstOrDefault();

                //Check if invoice detail not null then continue
                if (invoiceDetails != null)
                {
                    //Check reviewer is exists
                    var isExits = productRatingServices
                        .GetProductRatings(productRatingModel.ProductId, productRatingModel.ProductVariationId.Value)
                        .Where(x => x.InvoiceId == productRatingModel.InvoiceId);
                    //if not exists then continue
                    if (isExits == null)
                    {
                        var productRating = new ProductRating
                        {
                            IsTrash = productRatingModel.IsTrash,
                            Description = productRatingModel.Description,
                            isEdit = false,
                            ProductId = productRatingModel.ProductId,
                            ProductRatingId = productRatingModel.ProductRatingId,
                            ProductVariationId = productRatingModel.ProductVariationId,
                            Rating = productRatingModel.Rating,
                            UserId = this.UserId,

                        };

                        await productRatingServices.InsertProductRatingAsync(productRating);
                        return Ok(new ResponseApi
                        {
                            Message = new[] { "Insert Success Rating" },
                            Status = Ok().StatusCode,
                            Success = true
                        });
                    }

                    //if reviewer exists then return badrequest
                    return BadRequest(new ResponseApi
                    {
                        Message = new[] { "You reviewed" },
                        Status = BadRequest().StatusCode,
                        Success = false
                    });
                }
            }
            return BadRequest();
        }

        [HttpPut("[Action]")]
        [Authorize]
        public async Task<IActionResult> editProductRating(ProductRatingModel productRatingModel)
        {
            if (ModelState.IsValid)
            {
                var productRatingDb = await productRatingServices.GetProductRatingAsync(productRatingModel.Id);

                //Check is User 
                if (productRatingDb.UserId != this.UserId)
                {
                    return Unauthorized();
                }
                //Check rating is Edit
                if (productRatingDb.isEdit)
                {
                    //If true then not accept edit
                    return BadRequest(new ResponseApi
                    {
                        Message = new[] { "This Rating was Edited" },
                        Status = BadRequest().StatusCode,
                        Success = false
                    });
                }
                //if not
                //Update infomation rating
                productRatingDb.Description = productRatingModel.Description;
                productRatingDb.isEdit = true;
                productRatingDb.Rating = productRatingModel.Rating;

                await productRatingServices.UpdateProductRatingAsync(productRatingDb);

                return Ok(new ResponseApi
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
        public async Task<IActionResult> deleteProductRating(int id)
        {
            var productRating = await productRatingServices.GetProductRatingAsync(id);
            //Check is User of ProductRating
            //If not return unauthorized
            if (productRating.UserId != this.UserId)
                return Unauthorized();

            //If true then delete
            await productRatingServices.DeleteProductRatingAsync(id);
            return Ok(new ResponseApi
            {
                Message = new[] { "Delete Success Rating" },
                Status = Ok().StatusCode,
                Success = true
            });
        }
    }
}
