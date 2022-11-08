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

        //Get list cart from UserId
        [HttpGet]
        public IActionResult Carts(int? UserId)
        {
            //Check if Admin
            if (Library.isAdmin(roleName) || UserId == null)
                UserId = UserId ?? this.UserId;
            else
                return Unauthorized();

            //Get list cart for User 
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
                    x.ProductVariation,
                    totalMoney = x.Number * x.ProductVariation.PriceCurrent
                })
                .AsEnumerable();

            return Ok(new ResponseApi
            {
                Status = Ok().StatusCode,
                Data = new
                {
                    cart,
                    total = cart.Count(),
                    totalMoney = cart.Sum(x => x.Number * x.ProductVariation.PriceCurrent)
                },
                Success = true
            });
        }

        //Insert cart
        [HttpPost]
        public async Task<IActionResult> Cart(Cart cart)
        {
            if (ModelState.IsValid)
            {
                //Get and check card exits
                var cartDb = cartServices.GetCartByProduct(cart.ProductId, cart.ProductVarationId.Value, this.UserId);

                //If not exists then add
                if (cartDb == null)
                {
                    cart.UserId = this.UserId;
                    await cartServices.InsertCartAsync(cart);
                    int total = cartServices.GetCarts(this.UserId).Count();
                    return Ok(new ResponseApi
                    {
                        Success = true,
                        Status = Ok().StatusCode,
                        Data = new
                        {
                            cart,
                            total = total
                        }
                    });
                }
                //If exists increase Number for cart item
                cartDb.Number += cart.Number;
                await cartServices.UpdateCartAsync(cartDb);
                return Ok(new ResponseApi
                {
                    Message = new[] { "Add Success Product Exists in Cart" },
                    Status = Ok().StatusCode,
                    Data = new
                    {
                        cart = cartDb,
                        total = cartServices.GetCarts(this.UserId).Count()
                    },
                    Success = true
                });
            }
            return BadRequest();
        }


        [HttpPut("Cart")]
        public async Task<IActionResult> EditCart(Cart cart, int ProductVariationId)
        {
            if (ModelState.IsValid)
            {
                //Get and check cart exists
                var cartDb = cartServices.GetCartByProduct(cart.ProductId, ProductVariationId, UserId);
                //if have cart then update cart
                if (cartDb != null)
                {
                    cartDb.Number = cart.Number;
                    await cartServices.UpdateCartAsync(cartDb);
                    return Ok(new ResponseApi
                    {
                        Success = true,
                        Status = Ok().StatusCode,
                        Data = new
                        {
                            cart = cartDb,
                            total = cartDb.Number * cartDb.ProductVariation.PriceCurrent
                        },
                        Message = new[] { "Update success" }
                    });
                }
                //if not exists then result not found
                return NotFound(new ResponseApi
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
            //Delete cart from id and UserId
            await cartServices.DeleteCartAsync(id, UserId);
            return Ok(new ResponseApi
            {
                Message = new[] { "Delete Success" },
                Status = Ok().StatusCode,
                Success = true,
                Data = cartServices.GetCarts(this.UserId).Count()
            });
        }
    }
}
