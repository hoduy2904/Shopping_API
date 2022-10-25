using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfomationUserController : ControllerBase
    {
        private readonly IInfomationUserServices infomationUserServices;
        public InfomationUserController(IInfomationUserServices infomationUserServices)
        {
            this.infomationUserServices = infomationUserServices;
        }
        [HttpGet]
        public async Task<IActionResult> InfomationUsers()
        {
            var categories = await infomationUserServices.GetInfomationUsersAsync();
            return Ok(new ResultApi
            {
                Success = true,
                Data = categories
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> InfomationUser(int id)
        {
            try
            {
                var infomationUser = await infomationUserServices.GetInfomationUserAsync(id);
                if (infomationUser != null)
                    return Ok(new ResultApi
                    {
                        Data = infomationUser,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Success = false,
                    Message = "Not found infomation User"
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
        public IActionResult infomationUser(InfomationUser infomationUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    infomationUserServices.InsertInfomationUser(infomationUser);
                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Add Success",
                        Data = infomationUser
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

                    infomationUserServices.UpdateInfomationUser(infomationUserDb);

                    return Ok(new ResultApi
                    {
                        Success = true,
                        Message = "Edit success",
                        Data = infomationUserDb
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

        [HttpDelete("InfomationUser")]
        public IActionResult DeleteInfomationUser(int id)
        {
            try
            {
                infomationUserServices.DeleteInfomationUser(id);
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
