using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Extensions;
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

    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageServices productImageServices;
        public ProductImageController(IProductImageServices productImageServices)
        {
            this.productImageServices = productImageServices;
        }

        //Get productImages with paging
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ProductImages(int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var productImages = await productImageServices.GetProductImages()
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Success = true,
                Data = productImages,
                PageCount = productImages.PageCount,
                PageNumber = productImages.PageNumber,
                TotalItems = productImages.TotalItemCount
            });
        }

        //Get productImage from ProductImageId
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> ProductImage(int id)
        {
            var productImage = await productImageServices.GetProductImageAsync(id);

            if (productImage != null)
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Data = productImage,
                    Success = true
                });

            return NotFound(new ResponseApi
            {
                Status = (int)HttpStatusCode.NotFound,
                Success = false,
                Message = new[] { "Not found Product Image" }
            });
        }
        //Get ProducImage from productId
        [HttpGet("{ProductId}")]
        public IActionResult ProductImageByProductId(int ProductId)
        {
            var productImage = productImageServices.Where(x => x.ProductId == ProductId && x.IsTrash == false).AsEnumerable();
            return Ok(new ResponseApi
            {
                Status = (int)HttpStatusCode.OK,
                Data = productImage,
                Success = true
            });
        }


        //Insert ProductImage
        [HttpPost]
        public async Task<IActionResult> ProductImage(ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                await productImageServices.InsertProductImage(productImage);
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = productImage
                });

            }
            return BadRequest();
        }

        //update productImage
        [HttpPut("ProductImage")]
        public async Task<IActionResult> PutProductImage(ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                var ProductImageDb = await productImageServices.GetProductImageAsync(productImage.Id);

                ProductImageDb.Image = productImage.Image;

                await productImageServices.UpdateProductImage(ProductImageDb);

                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Edit success" },
                    Data = ProductImageDb
                });
            }
            return BadRequest();
        }
        //Delete productImage from productImageId

        [HttpDelete("ProductImage")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            await productImageServices.DeleteProductImage(id);
            return Ok(new ResponseApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
