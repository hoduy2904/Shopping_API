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
        private string folderRoot = Directory.GetCurrentDirectory() + "\\wwwroot";

        public ProductImageController(IProductImageServices productImageServices)
        {
            this.productImageServices = productImageServices;
        }

        //Get productImages with paging
        [HttpGet("[Action]"), AllowAnonymous]
        public async Task<IActionResult> getProductImages(int? page, int? pageSize)
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
        [HttpGet("[Action]/{id}"), AllowAnonymous]
        public async Task<IActionResult> getProductImage(int id)
        {
            var productImage = await productImageServices.GetProductImageAsync(id);
            productImage.Image = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + productImage.Image;
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
        [HttpGet("[Action]/{ProductId}")]
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
        [HttpPost("[Action]")]
        public async Task<IActionResult> insertProductImages([FromForm]ProductImage productImage, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                var productImages = new List<ProductImage>();
                if (images.Count > 0)
                {
                    foreach (var image in images)
                    {
                        string PathImage = await SaveImage(image);
                        productImages.Add(new ProductImage
                        {
                            Image = PathImage,
                            ProductId = productImage.ProductId,
                            ProductVariationId = productImage.ProductVariationId
                        });
                    }
                }
                await productImageServices.InsertProductImages(productImages);
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { $"Add Success {productImages.Count} items" },
                    Data = productImage
                });

            }
            return BadRequest();
        }

        //update productImage
        [HttpPut("[Action]")]
        public async Task<IActionResult> editProductImage([FromForm]ProductImage productImage, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var ProductImageDb = await productImageServices.GetProductImageAsync(productImage.Id);
                if (imageFile!=null && imageFile.Length > 0)
                {
                    string PathImage = await SaveImage(imageFile);
                    ProductImageDb.Image = PathImage;
                }
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

        [HttpDelete("[Action]")]
        public async Task<IActionResult> deleteProductImage(int id)
        {
            await productImageServices.DeleteProductImage(id);
            return Ok(new ResponseApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            string extensionFile = Path.GetExtension(image.FileName);
            string newFileImage = $"{Guid.NewGuid()}{extensionFile}";
            string PathImage = SaveFileConfig.Image + newFileImage;
            string fullPath = Path.Combine(folderRoot + SaveFileConfig.Image, newFileImage);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return PathImage;
        }
    }
}
