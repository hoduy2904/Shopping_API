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
        private string folderRoot = Directory.GetCurrentDirectory() + "\\wwwroot";
        private readonly IProductRatingImageServices productRatingImageServices;
        public ProductRatingImageController(IProductRatingImageServices productRatingImageServices)
        {
            this.productRatingImageServices = productRatingImageServices;
        }

        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> getProductRatingImage(int id)
        {
            var productRatingImage = await productRatingImageServices.GetProductRatingImageAsync(id);
            return Ok(new ResponseApi
            {
                Data = productRatingImage,
                Status = Ok().StatusCode,
                Success = true
            });
        }
        [HttpGet("[Action]/{ProductRatingId}")]
        public async Task<IActionResult> getProductRatingImagesByPrId(int ProductRatingId, int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var productRatingImages = await productRatingImageServices
                .GetProductRatingImagesByRatingId(ProductRatingId)
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Data = productRatingImages,
                PageCount = productRatingImages.PageCount,
                PageNumber = productRatingImages.PageNumber,
                TotalItems = productRatingImages.TotalItemCount,
                Status = Ok().StatusCode,
                Success = true
            });
        }
        [HttpPost("[Action]")]
        [Authorize]
        public async Task<IActionResult> insertProductRatingImages(int ProductRatingId, List<IFormFile> images)
        {
            //create list images
            var ProductRatingImages = new List<ProductRatingImage>();
            //Get all list image from user request
            foreach (var image in images)
            {
                //Save image to dictionary
                string uploadPath = await UploadImage(ProductRatingId, image);
                //add image to list
                ProductRatingImages.Add(new ProductRatingImage
                {
                    Image = uploadPath,
                    ProductRatingId = ProductRatingId
                });
            }
            //save to database
            await productRatingImageServices.InsertProductRatingImageRangeAsync(ProductRatingImages);

            return Ok(new ResponseApi
            {
                Status = Ok().StatusCode,
                Success = true,
                Data = ProductRatingImages,
                Message = new[] { "Insert success" }
            });
        }

        [HttpPut("[Action]")]
        [Authorize]
        public async Task<IActionResult> editProductRatingImages(List<ProductRatingImageRequest> productRatingImageRequests)
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

            return Ok(new ResponseApi
            {
                Status = Ok().StatusCode,
                Success = true,
                Data = ProductRatingImages,
                Message = new[] { "Update success" }
            });
        }

        [HttpDelete("[Action]")]
        [Authorize]
        public async Task<IActionResult> deleteProductRatingImages(int id)
        {
            await productRatingImageServices.DeleteProductRatingImageAsync(id);
            return Ok(new ResponseApi
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
            string uploadPath = SaveFileConfig.Image + fileName;
            string PathImage = Path.Combine(PathDirectory, fileName);
            using (var stream = new FileStream(PathImage, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return uploadPath;
        }
    }
}
