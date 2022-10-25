using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        public UserController(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await userServices.GetUsersAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = users
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> User(int id)
        {
            try
            {
                var user = await userServices.GetUserAsync(id);
                if (user != null)
                    return Ok(new ResultApi
                    {
                        Data = user,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Success = false,
                    Message = "Not found user"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult User(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userServices.InsertUser(user);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Add Success",
                        Data = user
                    });

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Success = false,
                    Message = ex.Message,
                    Data = user
                });
            }
        }

        [HttpPut("User")]
        public async Task<IActionResult> PutUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDb = await userServices.GetUserAsync(user.Id);

                    userDb.FristName = user.FristName;
                    userDb.LastName = user.LastName;
                    userDb.Email = user.Email;
                    userDb.IdentityCard = user.IdentityCard;
                    userDb.Sex = user.Sex;
                  

                    userServices.UpdateUser(userDb);

                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Edit success",
                        Data = userDb
                    });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("User")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                userServices.DeleteUser(id);
                return Ok(new ResultApi
                {
                    Success = true,
                    Message = "Delete Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
