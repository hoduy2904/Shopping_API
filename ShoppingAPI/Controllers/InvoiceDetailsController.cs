using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
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
    [Authorize]
    public class InvoiceDetailsController : ControllerBase
    {
        private readonly IInvoiceDetailsServices invoiceDetailsServices;
        private int UserId = -1;
        private readonly ICartServices cartServices;
        public InvoiceDetailsController(IInvoiceDetailsServices invoiceDetailsServices, IHttpContextAccessor httpContextAccessor, ICartServices cartServices)
        {
            this.invoiceDetailsServices = invoiceDetailsServices;
            this.UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            this.cartServices = cartServices;
        }

        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> getInvoiceDetail(int id)
        {
            var invoiceDetailDb = await invoiceDetailsServices.GetInvoiceDetailsAsync(id);
            return Ok(new ResponseApi
            {

                Status = Ok().StatusCode,
                Data = invoiceDetailDb,
                Success = true
            });
        }

        [HttpGet("[Action]/{InvoiceId}")]
        public async Task<IActionResult> getInvoiceDetails(int InvoiceId, int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var invoiceDetails = await invoiceDetailsServices
                .GetInvoicesDetailsByInvoiceId(InvoiceId)
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Data = invoiceDetails,
                PageCount = invoiceDetails.PageCount,
                PageNumber = invoiceDetails.PageNumber,
                TotalItems = invoiceDetails.TotalItemCount,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> insertInvoiceDetails(InvoiceDetailModel invoiceDetailModel)
        {
            var invoiceDetail = new InvoicesDetails
            {
                Image = invoiceDetailModel.Image,
                InvoiceId = invoiceDetailModel.InvoiceId,
                IsTrash = invoiceDetailModel.IsTrash,
                Numbers = invoiceDetailModel.Numbers,
                Price = invoiceDetailModel.Price,
                ProductId = invoiceDetailModel.ProductId,
                ProductName = invoiceDetailModel.ProductName,
                ProductVariationId = invoiceDetailModel.ProductVariationId,
            };

            await invoiceDetailsServices.InsertInvoiceDetailsAsync(invoiceDetail);
            return Ok(new ResponseApi
            {
                Data = invoiceDetail,
                Message = new[] { "Add Success" },
                Status = Ok().StatusCode,
                Success = true
            });

        }

        //Insert invoice with InvoiceId and Cartids
        [HttpPost("[Action]")]
        public async Task<IActionResult> insertInvoiceDetailsRangeByCardIds(int invoiceId, [FromBody] int[] cartids)
        {

            //Create list invoice details
            var lstinvoiceDetails = new List<InvoicesDetails>();

            //Get list cart from userId
            var carts = cartServices.GetCarts(this.UserId)
                .Include(p => p.ProductVariation)
                .ThenInclude(pi => pi.ProductImages)
                .Include(p => p.Product);

            //Loop cart ids from user request
            foreach (var id in cartids)
            {
                //get cart item from id
                var cart = carts.FirstOrDefault(x => x.Id == id);
                //Check if cart item null then loop continue
                if (cart == null)
                    continue;

                //Create new invoice details and add to list
                var invoiceDetails = new InvoicesDetails
                {
                    Image = cart.ProductVariation.ProductImages == null ? "" : cart.ProductVariation.ProductImages.FirstOrDefault().Image,
                    InvoiceId = invoiceId,
                    Numbers = cart.Number,
                    ProductName = cart.Product.Name,
                    Price = cart.ProductVariation.PriceCurrent,
                    ProductId = cart.ProductId,
                    ProductVariationId = cart.ProductVarationId,
                };
                lstinvoiceDetails.Add(invoiceDetails);
            }

            //Complete loop then save to database
            await invoiceDetailsServices.InsertInvoiceDetailRangesAsync(lstinvoiceDetails);

            //Delete cart item from cart
            foreach (var id in cartids)
            {
                await cartServices.DeleteCartAsync(id, this.UserId);
            }

            return Ok(new ResponseApi
            {
                Data = lstinvoiceDetails,
                Message = new[] { "Add Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> insertInvoiceDetailsRange(List<InvoiceDetailModel> invoiceDetailModels)
        {
            //Create list invoice details
            List<InvoicesDetails> invoicesDetails = new List<InvoicesDetails>();

            //Loop invoice detail from user request
            foreach (var invoiceDetail in invoiceDetailModels)
            {
                //create new Invoicde details and add to list
                invoicesDetails.Add(new InvoicesDetails
                {
                    ProductVariationId = invoiceDetail.ProductVariationId,
                    Image = invoiceDetail.Image,
                    InvoiceId = invoiceDetail.InvoiceId,
                    IsTrash = invoiceDetail.IsTrash,
                    Numbers = invoiceDetail.Numbers,
                    ProductId = invoiceDetail.ProductId,
                    Price = invoiceDetail.Price,
                    ProductName = invoiceDetail.ProductName,
                });
            }
            await invoiceDetailsServices.InsertInvoiceDetailRangesAsync(invoicesDetails);

            return Ok(new ResponseApi
            {
                Data = invoicesDetails,
                Message = new[] { "Add Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpPut("[Action]")]
        public async Task<IActionResult> editInvoiceDetails(InvoiceDetailModel invoiceDetailModel)
        {
            var invoicesDetails = new InvoicesDetails
            {
                Id = invoiceDetailModel.Id,
                ProductName = invoiceDetailModel.ProductName,
                Price = invoiceDetailModel.Price,
                ProductId = invoiceDetailModel.ProductId,
                Numbers = invoiceDetailModel.Numbers,
                Image = invoiceDetailModel.Image,
                InvoiceId = invoiceDetailModel.InvoiceId,
                IsTrash = invoiceDetailModel.IsTrash,
                ProductVariationId = invoiceDetailModel.ProductVariationId,
            };

            await invoiceDetailsServices.UpdateInvoiceDetailsAsync(invoicesDetails);
            return Ok(new ResponseApi
            {
                Data = invoicesDetails,
                Message = new[] { "Update Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpDelete("[Action]")]
        public async Task<IActionResult> deleteInvoiceDetails(int id)
        {
            await invoiceDetailsServices.DeleteInvoiceDetailsAsync(id);
            return Ok(new ResponseApi
            {
                Message = new[] { "Delete Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

    }
}
