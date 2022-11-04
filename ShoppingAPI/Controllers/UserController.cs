using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using ShoppingAPI.Common;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Net;
using System.Security.Claims;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly IRoleServices roleServices;
        public UserController(IUserServices userServices, IRoleServices roleServices)
        {
            this.roleServices = roleServices;
            this.userServices = userServices;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Users()
        {
            var users = await userServices.GetUsersAsync();
            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = users
            });
        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> User(int? id)
        {
            string roles = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value ?? "";
            var UserId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
            if (Library.isAdmin(roles) || UserId.Equals(id.ToString()) || id == null)
            {
                if (id == null)
                    id = int.Parse(UserId);
                var user = await userServices.GetUserAsync(id.Value);
                if (user != null)
                    return Ok(new ResultApi
                    {
                        Status = (int)HttpStatusCode.OK,
                        Data = user,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Success = false,
                    Message = new[] { "Not found user" }
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> User(User user, string? roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                roleName = "User";
            }
            if (ModelState.IsValid)
            {
                var role = roleServices.Where(x => x.Name.Equals(roleName)).FirstOrDefault();

                if (role != null)
                {
                    user.UserRoles = new List<UserRole>{
                            new UserRole
                            {
                                UserId = user.Id,
                                RoleId=role.Id
                            }
                        };
                }

                await userServices.InsertUser(user);
                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = user
                });

            }
            return BadRequest();
        }

        [HttpPut("User")]
        public async Task<IActionResult> PutUser(User user)
        {
            if (ModelState.IsValid)
            {
                var userDb = await userServices.GetUserAsync(user.Id);

                userDb.FristName = user.FristName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
                userDb.IdentityCard = user.IdentityCard;
                userDb.Sex = user.Sex;


                await userServices.UpdateUser(userDb);

                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Edit success" },
                    Data = userDb
                });
            }
            return BadRequest();
        }

        [HttpDelete("User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await userServices.DeleteUser(id);
            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });

        }
    }
}
