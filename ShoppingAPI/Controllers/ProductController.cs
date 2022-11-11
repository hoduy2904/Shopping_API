using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Data;
using System.Net;
using ShoppingAPI.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Model;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;
        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        //Get all products with paging
        [HttpGet("[Action]"), AllowAnonymous]
        public async Task<IActionResult> getProducts(int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var products = await productServices
                                .GetProducts()
                                .OrderByDescending(od => od.Id)
                                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = products,
                PageCount = products.PageCount,
                PageNumber = products.PageNumber,
                TotalItems = products.TotalItemCount
            });
        }

        //Get product
        [HttpGet("[Action]/{id}"), AllowAnonymous]
        public async Task<IActionResult> getProduct(int id)
        {
            var product = await productServices.GetProductAsync(id);
            if (product != null)
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Data = product,
                    Success = true
                });
            return NotFound(new ResponseApi
            {
                Status = (int)HttpStatusCode.NotFound,
                Success = false,
                Message = new[] { "Not found product" }
            });
        }

        //Insert Product
        [HttpPost("[Action]")]
        public async Task<IActionResult> insertProduct(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    CategoryId = productModel.CategoryId,
                    Description = productModel.Description,
                    IsTrash = productModel.IsTrash,
                    Name = productModel.Name,
                    SKUS = productModel.SKUS,
                };

                await productServices.InsertProduct(product);
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = product
                });

            }
            return BadRequest();
        }

        //Update product
        [HttpPut("[Action]")]
        public async Task<IActionResult> editProduct(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var productDb = await productServices.GetProductAsync(productModel.Id);

                productDb.CategoryId = productModel.CategoryId;
                productDb.Name = productModel.Name;
                productDb.Description = productModel.Description;
                productDb.IsTrash = productModel.IsTrash;

                await productServices.UpdateProduct(productDb);

                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Edit success" },
                    Data = productDb
                });
            }
            return BadRequest();
        }

        //Delete product from ProductId
        [HttpDelete("[Action]")]
        public async Task<IActionResult> deleteProduct(int id)
        {
            await productServices.DeleteProduct(id);
            return Ok(new ResponseApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
