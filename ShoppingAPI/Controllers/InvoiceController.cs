using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common;
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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices invoiceServices;
        private int UserId = -1;
        private string roleName = "Guest";
        public InvoiceController(IInvoiceServices invoiceServices, IHttpContextAccessor httpContextAccessor)
        {
            this.invoiceServices = invoiceServices;
            this.UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            this.roleName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        //Get invoice from InvoiceId
        [HttpGet("{id}")]
        public async Task<IActionResult> Invoice(int id)
        {
            var invoice = await invoiceServices.GetInvoiceAsync(id);
            //Check if invoice for User or Admin
            if (invoice.UserId == UserId || Library.isAdmin(roleName))
                return Ok(new ResponseApi
                {
                    Data = invoice,
                    Status = Ok().StatusCode,
                    Success = true
                });
            return BadRequest();
        }

        //Get list Invoices from UserId
        [HttpGet("{UserId}")]
        public async Task<IActionResult> Invoices(int UserId, int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            //Check if is User or Admin get
            if (this.UserId == UserId || Library.isAdmin(roleName))
            {
                var invoices = await invoiceServices.GetInvoicesByUserId(UserId)
                    .OrderByDescending(x => x.Id)
                    .ToPagedList(page.Value, pageSize.Value);

                return Ok(new ResponseWithPaging
                {
                    Data = invoices,
                    PageCount = invoices.PageCount,
                    PageNumber = invoices.PageNumber,
                    TotalItems = invoices.TotalItemCount,
                    Success = true,
                    Status = Ok().StatusCode
                });
            }
            //return unauthorized
            return Unauthorized();
        }

        //Insert Invoice

        [HttpPost("Invoice")]
        public async Task<IActionResult> Invoice(Invoice invoice)
        {
            //isIs User Post for User Invoice
            if (invoice.UserId == this.UserId)
            {
                await invoiceServices.InsertInvoiceAsync(invoice);
                return Ok(new ResponseApi
                {
                    Message = new[] { "Add Success" },
                    Status = Ok().StatusCode,
                    Data = invoice,
                    Success = true
                });
            }
            return BadRequest();
        }

        //Update Invoice
        [HttpPut("Invoice")]
        public async Task<IActionResult> PutInvoice(Invoice invoice)
        {
            //isIs User Update for User Invoice
            if (invoice.UserId == this.UserId)
            {
                await invoiceServices.UpdateInvoiceAsync(invoice);
                return Ok(new ResponseApi
                {
                    Message = new[] { "Update Success" },
                    Status = Ok().StatusCode,
                    Data = invoice,
                    Success = true
                });
            }
            return BadRequest();
        }

        //Delete Invoice 
        [HttpDelete("Invoice")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await invoiceServices.GetInvoiceAsync(id);
            //Check is User delete for User invoice
            if (invoice.UserId == this.UserId)
            {
                await invoiceServices.DeleteInvoiceAsync(id);
                return Ok(new ResponseApi
                {
                    Message = new[] { "Delete Success" },
                    Status = Ok().StatusCode,
                    Success = true
                });
            }
            //return unauthorized if not
            return Unauthorized();
        }
    }
}
