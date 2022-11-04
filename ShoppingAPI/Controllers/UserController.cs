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
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly IRoleServices roleServices;
        private int UserId = -1;
        private string roleName = "Guest";
        public UserController(IUserServices userServices, IRoleServices roleServices, IHttpContextAccessor httpContextAccessor)
        {
            this.roleServices = roleServices;
            this.userServices = userServices;
            this.UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            this.roleName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
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

        [HttpGet("[action]")]
        public async Task<IActionResult> User(int? id)
        {
            string roles = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value ?? "";
            var UserId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
            //Check if true user or Admin
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
            //if not
            return Unauthorized();
        }

        //Insert User
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> User(User user, string? roleName)
        {
            //Check if roleName null then assign by User
            if (string.IsNullOrEmpty(roleName))
            {
                roleName = "User";
            }

            if (ModelState.IsValid)
            {
                var role = roleServices.Where(x => x.Name.Equals(roleName)).FirstOrDefault();

                //check role exists
                if (role != null)
                {
                    user.UserRoles = new List<UserRole>
                    {
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

        //Edit User
        [HttpPut("User")]
        public async Task<IActionResult> PutUser(User user)
        {
            if (ModelState.IsValid)
            {
                //Check is User or Admin
                if (user.Id == this.UserId || Library.isAdmin(roleName))
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
                //if not
                return Unauthorized();
            }
            return BadRequest();
        }

        //Delete User
        [HttpDelete("User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //Check is User or Admin
            if (id == this.UserId || Library.isAdmin(roleName))
            {
                await userServices.DeleteUser(id);
                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Delete Success" }
                });
            }
            //if not
            return Unauthorized();
        }
    }
}
