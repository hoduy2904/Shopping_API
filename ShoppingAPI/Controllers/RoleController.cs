using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public class RoleController : ControllerBase
    {
        private readonly IRoleServices roleServices;
        public RoleController(IRoleServices roleServices)
        {
            this.roleServices = roleServices;
        }

        //Get all roles
        [HttpGet("[Action]"), AllowAnonymous]
        public async Task<IActionResult> getRoles(int? page, int? pageSize)
        {
            if (page == null)
                page = PagingSettingsConfig.pageDefault;
            if (pageSize == null)
                pageSize = PagingSettingsConfig.pageSize;

            var roles = await roleServices.GetRoles()
                .OrderByDescending(x => x.Id)
                .ToPagedList(page.Value, pageSize.Value);

            return Ok(new ResponseWithPaging
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Data = roles,
                PageCount = roles.PageCount,
                PageNumber = roles.PageNumber,
                TotalItems = roles.TotalItemCount
            });
        }

        //Get Role from RoleId
        [HttpGet("[Action]/{id}"), AllowAnonymous]
        public async Task<IActionResult> getRole(int id)
        {
            var Role = await roleServices.GetRoleAsync(id);

            if (Role != null)
                return Ok(new ResponseApi
                {
                    Status = (int)HttpStatusCode.OK,
                    Data = Role,
                    Success = true
                });

            return NotFound(new ResponseApi
            {
                Status = (int)HttpStatusCode.NotFound,
                Success = false,
                Message = new[] { "Not found Role" }
            });
        }

        //Insert Role
        [HttpPost("[Action]")]
        public async Task<IActionResult> insertRole(RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Name = roleModel.Name,
                    IsTrash = roleModel.IsTrash
                };

                await roleServices.InsertRole(role);
                return Ok(new ResponseApi
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
        [HttpPut("[Action]")]
        public async Task<IActionResult> editRole(RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var RoleDb = await roleServices.GetRoleAsync(roleModel.Id);

                RoleDb.Name = roleModel.Name;

                await roleServices.UpdateRole(RoleDb);

                return Ok(new ResponseApi
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
        [HttpDelete("[Action]")]
        public async Task<IActionResult> deleteRole(int id)
        {
            await roleServices.DeleteRole(id);
            return Ok(new ResponseApi
            {
                Status = (int)HttpStatusCode.OK,
                Success = true,
                Message = new[] { "Delete Success" }
            });
        }
    }
}
