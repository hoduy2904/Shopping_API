using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ShoppingDeliveryAddressController : ControllerBase
    {
        private readonly IShoppingDeliveryAddressServices shoppingDeliveryAddressServices;
        public ShoppingDeliveryAddressController(IShoppingDeliveryAddressServices shoppingDeliveryAddressServices)
        {
            this.shoppingDeliveryAddressServices = shoppingDeliveryAddressServices;
        }
        [HttpGet]
        public async Task<IActionResult> ShoppingDeliveryAddresses()
        {
            var categories = await shoppingDeliveryAddressServices.GetShoppingDeliveryAddressesAsync();
            return Ok(new ResultApi
            {
                Status = 200,
                Success = true,
                Data = categories
            });
        }
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> ShoppingDeliveryAddress(int id)
        {
            string roles = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value ?? "";
            var UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var shoppingDeliveryAddress = await shoppingDeliveryAddressServices.GetShoppingDeliveryAddressAsync(id);
            if (shoppingDeliveryAddress != null)
            {
                if (roles.Equals("SuperAdmin") || roles.Equals("Admin") || UserId.Equals(shoppingDeliveryAddress.UserId.ToString()))
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Data = shoppingDeliveryAddress,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Success = false,
                    Message = new[] { "Not found shoppingDeliveryAddress User" }
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> shoppingDeliveryAddress(ShoppingDeliveryAddress shoppingDelivery)
        {
            if (ModelState.IsValid)
            {
                await shoppingDeliveryAddressServices.InsertShoppingDeliveryAddress(shoppingDelivery);
                return Ok(new ResultApi
                {
                    Status = 200,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = shoppingDelivery
                });

            }
            return BadRequest();
        }

        [HttpPut("InfomationUser")]
        public async Task<IActionResult> PutInfomationUser(ShoppingDeliveryAddress shoppingDelivery)
        {
            if (ModelState.IsValid)
            {
                var shoppingDeliveryAddressDb = await shoppingDeliveryAddressServices.GetShoppingDeliveryAddressAsync(shoppingDelivery.Id);

                shoppingDeliveryAddressDb.PhoneNumber = shoppingDelivery.PhoneNumber;
                shoppingDeliveryAddressDb.Address = shoppingDelivery.Address;

                await shoppingDeliveryAddressServices.UpdateShoppingDeliveryAddress(shoppingDeliveryAddressDb);

                return Ok(new ResultApi
                {
                    Status = 200,
                    Success = true,
                    Message = new[] { "Edit success" },
                    Data = shoppingDeliveryAddressDb
                });
            }
            return BadRequest();
        }

        [HttpDelete("InfomationUser")]
        public async Task<IActionResult> DeleteShoppingDeliveryAddress(int id)
        {
            await shoppingDeliveryAddressServices.DeleteShoppingDeliveryAddress(id);
            return Ok(new ResultApi
            {
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
