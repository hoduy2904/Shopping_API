﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public InvoiceController(IInvoiceServices invoiceServices, IHttpContextAccessor httpContextAccessor)
        {
            this.invoiceServices = invoiceServices;
            this.UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Invoice(int id)
        {
            var invoice = await invoiceServices.GetInvoiceAsync(id);
            return Ok(new ResultApi
            {
                Data = invoice,
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpGet("{UserId}")]
        public IActionResult Invoices(int UserId)
        {
            if (this.UserId == UserId)
            {
                var invoices = invoiceServices.GetInvoicesByUserId(UserId).AsEnumerable();
                return Ok(new ResultApi
                {
                    Data = invoices,
                    Success = true,
                    Status = Ok().StatusCode
                });
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Invoice(Invoice invoice)
        {
            if (invoice.UserId == this.UserId)
            {
                await invoiceServices.InsertInvoiceAsync(invoice);
                return Ok(new ResultApi
                {
                    Message = new[] { "Add Success" },
                    Status = Ok().StatusCode,
                    Data = invoice,
                    Success = true
                });
            }
            return BadRequest();
        }

        [HttpPut("Invoice")]
        public async Task<IActionResult> PutInvoice(Invoice invoice)
        {
            if (invoice.UserId == this.UserId)
            {
                await invoiceServices.UpdateInvoiceAsync(invoice);
                return Ok(new ResultApi
                {
                    Message = new[] { "Update Success" },
                    Status = Ok().StatusCode,
                    Data = invoice,
                    Success = true
                });
            }
            return BadRequest();
        }

        [HttpDelete("Invoice")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await invoiceServices.GetInvoiceAsync(id);
            if (invoice.UserId == this.UserId)
            {
                await invoiceServices.DeleteInvoiceAsync(id);
                return Ok(new ResultApi
                {
                    Message = new[] { "Delete Success" },
                    Status = Ok().StatusCode,
                    Success = true
                });
            }
            return BadRequest();
        }
    }
}