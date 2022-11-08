using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Common;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Extensions;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingDeliveryAddressController : ControllerBase
    {
        private readonly IShoppingDeliveryAddressServices shoppingDeliveryAddressServices;
        private int UserId = -1;
        private string roleName = "Guest";
        public ShoppingDeliveryAddressController(IShoppingDeliveryAddressServices shoppingDeliveryAddressServices, IHttpContextAccessor httpContextAccessor)
        {
            this.shoppingDeliveryAddressServices = shoppingDeliveryAddressServices;
            this.UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            this.roleName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        [HttpGet]
        public async Task<IActionResult> ShoppingDeliveryAddresses(int? UserId, int? page, int? pageSize)
        {
            if (Library.isAdmin(roleName))
                this.UserId = UserId ?? this.UserId;

            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var DeliveryAddress = await shoppingDeliveryAddressServices
                .GetShoppingDeliveryAddresses()
                .Where(x => x.UserId == this.UserId)
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = DeliveryAddress,
                PageCount = DeliveryAddress.PageCount,
                PageNumber = DeliveryAddress.PageNumber,
                TotalItems = DeliveryAddress.TotalItemCount
            });
        }

        //Get ShoppingDeliveryAddress from Id
        [HttpGet("{id}")]
        public async Task<IActionResult> ShoppingDeliveryAddress(int id)
        {
            var shoppingDeliveryAddress = await shoppingDeliveryAddressServices.GetShoppingDeliveryAddressAsync(id);

            //Check ShoppingDeliveryAddress Exists
            if (shoppingDeliveryAddress != null)
            {
                //check if admin or user for Address user
                if (Library.isAdmin(roleName) || UserId.Equals(shoppingDeliveryAddress.UserId.ToString()))
                    return Ok(new ResponseApi
                    {
                        Status = (int)HttpStatusCode.OK,
                        Data = shoppingDeliveryAddress,
                        Success = true
                    });

                return NotFound(new ResponseApi
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Success = false,
                    Message = new[] { "Not found shoppingDeliveryAddress User" }
                });
            }
            return Unauthorized();
        }

        //Insert ShoppingDelivery Address by User
        [HttpPost]
        public async Task<IActionResult> shoppingDeliveryAddress(ShoppingDeliveryAddress shoppingDelivery)
        {
            if (ModelState.IsValid)
            {
                shoppingDelivery.UserId = this.UserId;
                await shoppingDeliveryAddressServices.InsertShoppingDeliveryAddress(shoppingDelivery);
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = shoppingDelivery
                });

            }
            return BadRequest();
        }
        //Update Shopping DeliveryAddress by User

        [HttpPut("ShoppingDeliveryAddress")]
        public async Task<IActionResult> PutInfomationUser(ShoppingDeliveryAddress shoppingDelivery)
        {
            if (ModelState.IsValid)
            {
                if (this.UserId == shoppingDelivery.UserId)
                {
                    var shoppingDeliveryAddressDb = await shoppingDeliveryAddressServices.GetShoppingDeliveryAddressAsync(shoppingDelivery.Id);

                    shoppingDeliveryAddressDb.PhoneNumber = shoppingDelivery.PhoneNumber;
                    shoppingDeliveryAddressDb.Address = shoppingDelivery.Address;

                    await shoppingDeliveryAddressServices.UpdateShoppingDeliveryAddress(shoppingDeliveryAddressDb);

                    return Ok(new ResponseApi
                    {
                        Status = (int)HttpStatusCode.OK,
                        Success = true,
                        Message = new[] { "Edit success" },
                        Data = shoppingDeliveryAddressDb
                    });
                }
            }
            return Unauthorized();
        }

        //Delete deleteShoppingDeliveryAddress

        [HttpDelete("ShoppingDeliveryAddress")]
        public async Task<IActionResult> DeleteShoppingDeliveryAddress(int id)
        {
            var shoppingbyUser = await shoppingDeliveryAddressServices.GetShoppingDeliveryAddressAsync(id);
            //check if true user
            if (shoppingbyUser.UserId == this.UserId)
            {
                await shoppingDeliveryAddressServices.DeleteShoppingDeliveryAddress(id);
                return Ok(new ResponseApi
                {
                    Success = true,
                    Message = new[] { "Delete Success" }
                });
            }
            return Unauthorized();
        }
    }
}
