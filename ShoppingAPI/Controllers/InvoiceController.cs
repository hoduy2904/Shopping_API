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
        private readonly IShoppingDeliveryAddressServices shoppingDeliveryAddressServices;
        private int UserId = -1;
        private string roleName = "Guest";
        public InvoiceController(IInvoiceServices invoiceServices, IHttpContextAccessor httpContextAccessor, IShoppingDeliveryAddressServices shoppingDeliveryAddressServices)
        {
            this.invoiceServices = invoiceServices;
            this.UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            this.roleName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            this.shoppingDeliveryAddressServices = shoppingDeliveryAddressServices;
        }

        //Get invoice from InvoiceId
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> getInvoice(int id)
        {
            var invoice = await invoiceServices.GetInvoiceAsync(id);

            if (invoice.UserId != this.UserId)
                return Unauthorized();

            //Check if has invoice for id
            if (invoice == null)
                return NotFound(new ResponseApi
                {
                    Message = new[] { $"Not found Invoice for id: {id}" },
                    Status = NotFound().StatusCode,
                    Success = false
                });

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
        [HttpGet("[Action]")]
        public async Task<IActionResult> getInvoices(int? UserId, int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            if (UserId == null)
                UserId = this.UserId;

            //Check if is User or Admin get
            if (this.UserId == UserId || Library.isAdmin(roleName))
            {
                var invoices = await invoiceServices
                    .GetInvoicesByUserId(UserId.Value)
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

        //Insert Invoice with DeliveryId

        [HttpPost("[Action]")]
        public async Task<IActionResult> insertInvoice(int DeliveryId)
        {
            //Get deliveryid
            var deliveryId = await shoppingDeliveryAddressServices.GetShoppingDeliveryAddressAsync(DeliveryId);

            //Check if true Userid
            if (deliveryId.UserId == this.UserId)
            {
                var Invoice = new Invoice
                {
                    UserId = this.UserId,
                    Address = deliveryId.Address,
                    FullName = deliveryId.FullName,
                    PhoneNumber = deliveryId.PhoneNumber
                };
                await invoiceServices.InsertInvoiceAsync(Invoice);
                return Ok(new ResponseApi
                {
                    Message = new[] { "Add Success" },
                    Status = Ok().StatusCode,
                    Data = Invoice,
                    Success = true
                });
            }

            return Unauthorized();
        }

        //Update Invoice
        [HttpPut("[Action]")]
        public async Task<IActionResult> editInvoice(Invoice invoice)
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
        [HttpDelete("[Action]")]
        public async Task<IActionResult> deleteInvoice(int id)
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
