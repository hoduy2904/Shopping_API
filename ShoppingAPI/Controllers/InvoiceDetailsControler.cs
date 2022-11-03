﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var invoiceDetail = await invoiceDetailsServices.GetInvoiceDetailsAsync(id);
            return Ok(new ResultApi
            {

                Status = Ok().StatusCode,
                Data = invoiceDetail,
                Success = true
            });
        }
        [HttpGet("{InvoiceId}")]
        public IActionResult InvoiceDetails(int InvoiceId)
        {
            var invoiceDetails = invoiceDetailsServices.GetInvoicesDetailsByInvoiceId(InvoiceId).AsEnumerable();
            return Ok(new ResultApi
            {
                Data = invoiceDetails,
                Status = Ok().StatusCode,
                Success = true
            });

        }
        [HttpPost]
        public async Task<IActionResult> InvoiceDetails(InvoicesDetails invoicesDetails)
        {
            await invoiceDetailsServices.InsertInvoiceDetailsAsync(invoicesDetails);
            return Ok(new ResultApi
            {
                Data = invoicesDetails,
                Message = new[] { "Add Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpPost]
        public async Task<IActionResult> InvoiceDetailsRange(IEnumerable<InvoicesDetails> invoicesDetails)
        {
            await invoiceDetailsServices.InsertInvoiceDetailRangesAsync(invoicesDetails);
            return Ok(new ResultApi
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
            return Ok(new ResultApi
            {
                Data = invoicesDetails,
                Message = new[] { "Update Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

        [HttpPut("InvoiceDetails")]
        public async Task<IActionResult> DeleteInvoiceDetails(int id)
        {
            await invoiceDetailsServices.DeleteInvoiceDetailsAsync(id);
            return Ok(new ResultApi
            {
                Message = new[] { "Delete Success" },
                Status = Ok().StatusCode,
                Success = true
            });
        }

    }
}