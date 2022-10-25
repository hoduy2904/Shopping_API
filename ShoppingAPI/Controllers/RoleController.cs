using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices roleServices;
        public RoleController(IRoleServices roleServices)
        {
            this.roleServices = roleServices;
        }
        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var roles = await roleServices.GetRolesAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = roles
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Role(int id)
        {
            try
            {
                var Role = await roleServices.GetRoleAsync(id);
                if (Role != null)
                    return Ok(new ResultApi
                    {
                        Data = Role,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Success = false,
                    Message = "Not found Role"
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
        public IActionResult Role(Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    roleServices.InsertRole(role);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Add Success",
                        Data = role
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

                    roleServices.UpdateRole(RoleDb);

                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Edit success",
                        Data = RoleDb
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

        [HttpDelete("Role")]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                roleServices.DeleteRole(id);
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
