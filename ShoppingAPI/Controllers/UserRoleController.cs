using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly  IUserRoleServices userRoleServices;
        public UserRoleController(IUserRoleServices userRoleServices)
        {
            this.userRoleServices = userRoleServices;
        }
        [HttpGet]
        public async Task<IActionResult> UserRoles()
        {
            var userRoles = await userRoleServices.GetUserRolesAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = userRoles
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> UserRole(int id)
        {
            try
            {
                var userRole = await userRoleServices.GetUserRoleAsync(id);
                if (userRole != null)
                    return Ok(new ResultApi
                    {
                        Data = userRole,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Success = false,
                    Message = "Not found user Role"
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
        public IActionResult UserRole(UserRole userRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userRoleServices.InsertUserRole(userRole);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Add Success",
                        Data = userRole
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
                        Success = true,
                        Message = "Edit success",
                        Data = userRoleDb
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

        [HttpDelete("UserRole")]
        public IActionResult DeleteUserRole(int id)
        {
            try
            {
                userRoleServices.DeleteUserRole(id);
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
