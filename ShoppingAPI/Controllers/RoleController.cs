using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Data;
using System.Net;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class RoleController : ControllerBase
    {
        private readonly IRoleServices roleServices;
        public RoleController(IRoleServices roleServices)
        {
            this.roleServices = roleServices;
        }
        [HttpGet,AllowAnonymous]
        public async Task<IActionResult> Roles()
        {
            var roles = await roleServices.GetRolesAsync();
            return Ok(new ResultApi
            {
                Status = 200,
                Success = true,
                Data = roles
            });
        }
        [HttpGet("{id}"),AllowAnonymous]
        public async Task<IActionResult> Role(int id)
        {
            try
            {
                var Role = await roleServices.GetRoleAsync(id);
                if (Role != null)
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Data = Role,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Success = false,
                    Message = new[] { "Not found Role" }
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
        public async Task<IActionResult> Role(Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await roleServices.InsertRole(role);
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Add Success" },
                        Data = role
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
                    Data = role
                });
            }
        }

        [HttpPut("Role")]
        public async Task<IActionResult> PutRole(Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var RoleDb = await roleServices.GetRoleAsync(role.Id);

                    RoleDb.Name = role.Name;

                   await roleServices.UpdateRole(RoleDb);

                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Edit success" },
                        Data = RoleDb
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

        [HttpDelete("Role")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
               await roleServices.DeleteRole(id);
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
