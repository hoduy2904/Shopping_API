using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Extensions;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Model;
using ShoppingAPI.Services.Interfaces;
using System.Data;
using System.Net;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleServices userRoleServices;
        public UserRoleController(IUserRoleServices userRoleServices)
        {
            this.userRoleServices = userRoleServices;
        }

        //Get UserRoles
        [HttpGet("[Action]"), AllowAnonymous]
        public async Task<IActionResult> getUserRoles(int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var userRoles = await userRoleServices
                .GetUserRoles()
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = userRoles,
                PageCount = userRoles.PageCount,
                PageNumber = userRoles.PageNumber,
                TotalItems = userRoles.TotalItemCount
            });
        }

        //Get UserRole
        [HttpGet("[Action]/{id}"), AllowAnonymous]
        public async Task<IActionResult> getUserRole(int id)
        {
            var userRole = await userRoleServices.GetUserRoleAsync(id);

            if (userRole != null)
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Data = userRole,
                    Success = true
                });

            return NotFound(new ResponseApi
            {
                Status = NotFound().StatusCode,
                Success = false,
                Message = new[] { "Not found user Role" }
            });
        }

        //Insert UserRole
        [HttpPost("[Action]")]
        public async Task<IActionResult> insertUserRole(UserRoleModel userRoleModel)
        {
            if (ModelState.IsValid)
            {
                var userRole = new UserRole
                {
                    RoleId = userRoleModel.RoleId,
                    UserId = userRoleModel.UserId,
                };

                await userRoleServices.InsertUserRole(userRole);
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = userRole
                });

            }
            return BadRequest();
        }

        //Update UserRole
        [HttpPut("[Action]")]
        public async Task<IActionResult> editUserRole(UserRoleModel userRoleModel)
        {
            if (ModelState.IsValid)
            {
                var userRoleDb = await userRoleServices.GetUserRoleAsync(userRoleModel.Id);

                userRoleDb.RoleId = userRoleModel.RoleId;

                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Edit success" },
                    Data = userRoleDb
                });
            }
            return BadRequest();
        }

        //delete userole
        [HttpDelete("[Action]")]
        public async Task<IActionResult> deleteUserRole(int id)
        {
            await userRoleServices.DeleteUserRole(id);
            return Ok(new ResponseApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
