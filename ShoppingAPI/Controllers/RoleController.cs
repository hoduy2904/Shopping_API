using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Extensions;
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

    public class RoleController : ControllerBase
    {
        private readonly IRoleServices roleServices;
        public RoleController(IRoleServices roleServices)
        {
            this.roleServices = roleServices;
        }

        //Get all roles
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Roles(int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var roles = await roleServices.GetRoles()
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = new ResultWithPaging
                {
                    Data = roles,
                    PageCount = roles.PageCount,
                    PageNumber = roles.PageNumber,
                    TotalItems = roles.TotalItemCount
                }
            });
        }

        //Get Role from RoleId
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> Role(int id)
        {
            var Role = await roleServices.GetRoleAsync(id);

            if (Role != null)
                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
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

        //Insert Role
        [HttpPost]
        public async Task<IActionResult> Role(Role role)
        {
            if (ModelState.IsValid)
            {
                await roleServices.InsertRole(role);
                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Add Success" },
                    Data = role
                });

            }
            return BadRequest();
        }

        //Update Role
        [HttpPut("Role")]
        public async Task<IActionResult> PutRole(Role role)
        {
            if (ModelState.IsValid)
            {
                var RoleDb = await roleServices.GetRoleAsync(role.Id);

                RoleDb.Name = role.Name;

                await roleServices.UpdateRole(RoleDb);

                return Ok(new ResultApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = new[] { "Edit success" },
                    Data = RoleDb
                });
            }
            return BadRequest();
        }

        //Delete Role from RoleId
        [HttpDelete("Role")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await roleServices.DeleteRole(id);
            return Ok(new ResultApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
