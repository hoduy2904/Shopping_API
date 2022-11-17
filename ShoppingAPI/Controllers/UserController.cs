using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Extensions;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Model;
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

        [HttpGet("[Action]")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> getUsers(int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var users = await userServices.GetUsers()
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = users,
                PageCount = users.PageCount,
                PageNumber = users.PageNumber,
                TotalItems = users.TotalItemCount
            });
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> getUser(int? id)
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
                    return Ok(new ResponseApi
                    {
                        Status = (int)HttpStatusCode.OK,
                        Data = user,
                        Success = true
                    });

                return NotFound(new ResponseApi
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
        [HttpPost("[Action]")]
        public async Task<IActionResult> insertUser(UserModel userModel)
        {
            var user = new User
            {
                Email = userModel.Email,
                FristName = userModel.FristName,
                IdentityCard = userModel.IdentityCard,
                IsTrash = false,
                LastName = userModel.LastName,
                Sex = userModel.Sex,
                Username = userModel.Username,
                PasswordHash = userModel.Password
            };

            //Check if roleName null then assign by User
            if (string.IsNullOrEmpty(userModel.RoleName))
            {
                userModel.RoleName = "User";
            }

            if (ModelState.IsValid)
            {
                var role = roleServices.Where(x => x.Name.Equals(userModel.RoleName)).FirstOrDefault();

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

                return Ok(new ResponseApi
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
        [HttpPut("[Action]")]
        public async Task<IActionResult> editUser(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                //Check is User or Admin
                if (userModel.Id == this.UserId || Library.isAdmin(roleName))
                {
                    var userDb = await userServices.GetUserAsync(userModel.Id);

                    userDb.FristName = userModel.FristName;
                    userDb.LastName = userModel.LastName;
                    userDb.Email = userModel.Email;
                    userDb.IdentityCard = userModel.IdentityCard;
                    userDb.Sex = userModel.Sex;

                    if (!string.IsNullOrEmpty(userModel.Password))
                    {
                        userDb.PasswordHash = StringHashing.Hash(userModel.Password);
                    }


                    await userServices.UpdateUser(userDb);

                    return Ok(new ResponseApi
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
        [HttpDelete("[Action]")]
        public async Task<IActionResult> deleteUser(int id)
        {
            //Check is User or Admin
            if (id == this.UserId || Library.isAdmin(roleName))
            {
                await userServices.DeleteUser(id);
                return Ok(new ResponseApi
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
