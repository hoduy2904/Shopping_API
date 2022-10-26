using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Net;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class InfomationUserController : ControllerBase
    {
        private readonly IInfomationUserServices infomationUserServices;
        public InfomationUserController(IInfomationUserServices infomationUserServices)
        {
            this.infomationUserServices = infomationUserServices;
        }
        [HttpGet,AllowAnonymous]
        public async Task<IActionResult> InfomationUsers()
        {
            var categories = await infomationUserServices.GetInfomationUsersAsync();
            return Ok(new ResultApi
            {
                Status = 200,
                Success = true,
                Data = categories
            });
        }
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> InfomationUser(int id)
        {
            try
            {
                var infomationUser = await infomationUserServices.GetInfomationUserAsync(id);
                if (infomationUser != null)
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Data = infomationUser,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Success = false,
                    Message = new[] { "Not found infomation User" }
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
        public async Task<IActionResult> infomationUser(InfomationUser infomationUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await infomationUserServices.InsertInfomationUser(infomationUser);
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Add Success" },
                        Data = infomationUser
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
                    Data = infomationUser
                });
            }
        }

        [HttpPut("InfomationUser")]
        public async Task<IActionResult> PutInfomationUser(InfomationUser infomationUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var infomationUserDb = await infomationUserServices.GetInfomationUserAsync(infomationUser.Id);

                    infomationUserDb.PhoneNumber = infomationUser.PhoneNumber;
                    infomationUserDb.Address = infomationUser.Address;

                   await infomationUserServices.UpdateInfomationUser(infomationUserDb);

                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Edit success" },
                        Data = infomationUserDb
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

        [HttpDelete("InfomationUser")]
        public async Task<IActionResult> DeleteInfomationUser(int id)
        {
            try
            {
               await infomationUserServices.DeleteInfomationUser(id);
                return Ok(new ResultApi
                {
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
