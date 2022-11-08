using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Extensions;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRatingImageController : ControllerBase
    {
        private readonly string PathDirectory = Directory.GetCurrentDirectory();
        private readonly string PathImageDirectory = "\\wwwroot\\Images\\";
        private readonly IProductRatingImageServices productRatingImageServices;
        public ProductRatingImageController(IProductRatingImageServices productRatingImageServices)
        {
            this.productRatingImageServices = productRatingImageServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProductRatingImage(int id)
        {
            var productRatingImage = await productRatingImageServices.GetProductRatingImageAsync(id);
            return Ok(new ResultApi
            {
                Data = productRatingImage,
                Status = Ok().StatusCode,
                Success = true
            });
        }
        [HttpGet("{ProductRatingId}")]
        public async Task<IActionResult> ProductRatingImages(int ProductRatingId, int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var productRatingImages = await productRatingImageServices
                .GetProductRatingImagesByRatingId(ProductRatingId)
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);
            return Ok(new ResultApi
            {
                Data = new ResultWithPaging
                {
                    Data = productRatingImages,
                    PageCount = productRatingImages.PageCount,
                    PageNumber = productRatingImages.PageNumber,
                    TotalItems = productRatingImages.TotalItemCount
                },
                Status = Ok().StatusCode,
                Success = true
            });
        }
        [HttpPost("[Action]")]
        [Authorize]
        public async Task<IActionResult> ProductRatingImages(int ProductRatingId, List<IFormFile> images)
        {
            var ProductRatingImages = new List<ProductRatingImage>();
            foreach (var image in images)
            {
                string uploadPath = await UploadImage(ProductRatingId, image);
                ProductRatingImages.Add(new ProductRatingImage
                {
                    Image = uploadPath,
                    ProductRatingId = ProductRatingId
                });
            }
            await productRatingImageServices.InsertProductRatingImageRangeAsync(ProductRatingImages);

            return Ok(new ResultApi
            {
                Status = Ok().StatusCode,
                Success = true,
                Data = ProductRatingImages,
                Message = new[] { "Insert success" }
            });
        }

        [HttpPut("[Action]")]
        [Authorize]
        public async Task<IActionResult> PutProductRatingImages(List<ProductRatingImageRequest> productRatingImageRequests)
        {
            var ProductRatingImages = new List<ProductRatingImage>();
            foreach (var productRatingImage in productRatingImageRequests)
            {
                string uploadPath = await UploadImage(productRatingImage.ProductImageRatingId, productRatingImage.Image);
                ProductRatingImages.Add(new ProductRatingImage
                {
                    ProductRatingId = productRatingImage.ProductImageRatingId,
                    Image = uploadPath
                });
            }

            await productRatingImageServices.UpdateProductRatingImageRangeAsync(ProductRatingImages);

            return Ok(new ResultApi
            {
                Status = Ok().StatusCode,
                Success = true,
                Data = ProductRatingImages,
                Message = new[] { "Update success" }
            });
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteProductRatingImages(int id)
        {
            await productRatingImageServices.DeleteProductRatingImageAsync(id);
            return Ok(new ResultApi
            {
                Status = Ok().StatusCode,
                Success = true,
                Message = new[] { "Delete success" }
            });
        }

        private async Task<string> UploadImage(int ProductRatingId, IFormFile image)
        {
            string extendsion = Path.GetExtension(image.FileName);
            var fileName = $"{Guid.NewGuid().ToString()}-{ProductRatingId}{extendsion}";
            string uploadPath = PathImageDirectory + fileName;
            string PathImage = Path.Combine(PathDirectory + PathImageDirectory, fileName);
            using (var stream = new FileStream(PathImage, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return uploadPath;
        }
    }
}
