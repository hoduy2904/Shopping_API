using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
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
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> UserRoles()
        {
            var userRoles = await userRoleServices.GetUserRolesAsync();
            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = userRoles
            });
        }

        //Get UserRole
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> UserRole(int id)
        {
            var userRole = await userRoleServices.GetUserRoleAsync(id);

            if (userRole != null)
                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Data = userRole,
                    Success = true
                });

            return NotFound(new ResultApi
            {
                Status = NotFound().StatusCode,
                Success = false,
                Message = new[] { "Not found user Role" }
            });
        }

        //Insert UserRole
        [HttpPost]
        public async Task<IActionResult> UserRole(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                await userRoleServices.InsertUserRole(userRole);
                return Ok(new ResultApi
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
        [HttpPut("UserRole")]
        public async Task<IActionResult> PutUserRole(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                var userRoleDb = await userRoleServices.GetUserRoleAsync(userRole.Id);

                userRoleDb.RoleId = userRole.RoleId;

                return Ok(new ResultApi
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
        [HttpDelete("UserRole")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            await userRoleServices.DeleteUserRole(id);
            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
