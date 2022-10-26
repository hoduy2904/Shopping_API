using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Data;

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
        [HttpGet,AllowAnonymous]
        public async Task<IActionResult> UserRoles()
        {
            var userRoles = await userRoleServices.GetUserRolesAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = userRoles
            });
        }
        [HttpGet("{id}"),AllowAnonymous]
        public async Task<IActionResult> UserRole(int id)
        {
            try
            {
                var userRole = await userRoleServices.GetUserRoleAsync(id);
                if (userRole != null)
                    return Ok(new ResultApi
                    {
                        Status = 200,
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
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Status = ex.HResult,
                    Success = false,
                    Message = new[] { ex.Message }
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserRole(UserRole userRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await userRoleServices.InsertUserRole(userRole);
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Add Success" },
                        Data = userRole
                    });

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Status = ex.HResult,
                    Success = false,
                    Message = new[] { ex.Message },
                    Data = userRole
                });
            }
        }

        [HttpPut("UserRole")]
        public async Task<IActionResult> PutUserRole(UserRole userRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userRoleDb = await userRoleServices.GetUserRoleAsync(userRole.Id);

                    userRoleDb.RoleId = userRole.RoleId;

                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Edit success" },
                        Data = userRoleDb
                    });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Status = ex.HResult,
                    Success = false,
                    Message = new[] { ex.Message }
                });
            }
        }

        [HttpDelete("UserRole")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            try
            {
               await userRoleServices.DeleteUserRole(id);
                return Ok(new ResultApi
                {
                    Status = 200,
                    Success = true,
                    Message = new[] { "Delete Success" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Status = ex.HResult,
                    Success = false,
                    Message = new[] { ex.Message }
                });
            }
        }
    }
}
