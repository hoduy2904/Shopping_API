using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Extensions;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Security.Claims;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceDetailsControler : ControllerBase
    {
        private readonly IInvoiceDetailsServices invoiceDetailsServices;
        private int UserId = -1;
        public InvoiceDetailsControler(IInvoiceDetailsServices invoiceDetailsServices, IHttpContextAccessor httpContextAccessor)
        {
            this.invoiceDetailsServices = invoiceDetailsServices;
            this.UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> InvoiceDetail(int id)
        {
            var invoiceDetailDb = await invoiceDetailsServices.GetInvoiceDetailsAsync(id);
            return Ok(new ResponseApi
            {

                Status = Ok().StatusCode,
                Data = invoiceDetailDb,
                Success = true
            });
        }

        [HttpGet("{InvoiceId}")]
        public async Task<IActionResult> InvoiceDetails(int InvoiceId, int? page, int? pageSize)
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

        [HttpPost]
        public async Task<IActionResult> InvoiceDetails(InvoicesDetails invoicesDetails)
        {
            await invoiceDetailsServices.InsertInvoiceDetailsAsync(invoicesDetails);
            return Ok(new ResponseApi
            {
                Data = invoicesDetails,
                Message = new[] { "Add Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InvoiceDetailsRange(IEnumerable<InvoicesDetails> invoicesDetails)
        {
            await invoiceDetailsServices.InsertInvoiceDetailRangesAsync(invoicesDetails);
            return Ok(new ResponseApi
            {
                Data = invoicesDetails,
                Message = new[] { "Add Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpPut("InvoiceDetails")]
        public async Task<IActionResult> PutInvoiceDetails(InvoicesDetails invoicesDetails)
        {
            await invoiceDetailsServices.UpdateInvoiceDetailsAsync(invoicesDetails);
            return Ok(new ResponseApi
            {
                Data = invoicesDetails,
                Message = new[] { "Update Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpDelete("InvoiceDetails")]
        public async Task<IActionResult> DeleteInvoiceDetails(int id)
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
