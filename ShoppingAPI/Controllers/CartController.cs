using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Security.Claims;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartServices cartServices;
        private int UserId = -1;
        string roleName = "Guest";
        public CartController(ICartServices cartServices, IHttpContextAccessor httpContextAccessor)
        {
            this.cartServices = cartServices;
            this.UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            this.roleName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        [HttpGet]
        public IActionResult Carts(int? UserId)
        {
            //Check if Admin
            if (Library.isAdmin(roleName))
                UserId = UserId ?? this.UserId;

            var cart = cartServices.GetCarts(this.UserId)
                .Include(pv => pv.ProductVariation)
                .ThenInclude(p => p.Product)
                .Include(pv => pv.ProductVariation)
                .ThenInclude(pi => pi.ProductImages)
                .Select(x => new
                {
                    x.Id,
                    x.UserId,
                    x.Created,
                    x.Number,
                    x.ProductVariation
                })
                .AsEnumerable();
            return Ok(new ResultApi
            {
                Status = Ok().StatusCode,
                Data = cart,
                Success = true
            });
        }
        [HttpPost]
        public async Task<IActionResult> Cart(Cart cart)
        {
            if (ModelState.IsValid)
            {
                var cartDb = cartServices.GetCartByProduct(cart.ProductId, cart.ProductVarationId.Value, this.UserId);
                if (cartDb == null)
                {
                    cart.UserId = this.UserId;
                    await cartServices.InsertCartAsync(cart);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Status = Ok().StatusCode,
                        Data = cart
                    });
                }
                cartDb.Number += cart.Number;
                await cartServices.UpdateCartAsync(cartDb);
                return Ok(new ResultApi
                {
                    Message = new[] { "Add Success Product Exists in Cart" },
                    Status = Ok().StatusCode,
                    Data = cartDb,
                    Success = true
                });
            }
            return BadRequest();
        }

        [HttpPut("Cart")]
        public async Task<IActionResult> EditCart(Cart cart)
        {
            if (ModelState.IsValid)
            {
                var cartDb = cartServices.GetCartByProduct(cart.ProductId, cart.ProductVarationId.Value, UserId);
                if (cartDb != null)
                {
                    cartDb.Number = cart.Number;
                    await cartServices.UpdateCartAsync(cartDb);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Status = Ok().StatusCode,
                        Data = cart,
                        Message = new[] { "Update success" }
                    });
                }
                return NotFound(new ResultApi
                {
                    Success = false,
                    Status = NotFound().StatusCode,
                    Message = new[] { "Not found product in Cart" }
                });
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> Cart(int id)
        {
            await cartServices.DeleteCartAsync(id, UserId);
            return Ok(new ResultApi
            {
                Message = new[] { "Delete Success" },
                Status = Ok().StatusCode,
                Success = true,
            });
        }
    }
}
