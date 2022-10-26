﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;
using System.Net;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        public UserController(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Users()
        {
            var users = await userServices.GetUsersAsync();
            return Ok(new ResultApi
            {
                Status = 200,
                Success = true,
                Data = users
            });
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> User(int id)
        {
            try
            {
                var user = await userServices.GetUserAsync(id);
                if (user != null)
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Data = user,
                        Success = true
                    });
                return NotFound(new ResultApi
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Success = false,
                    Message = new[] { "Not found user" }
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
        public async Task<IActionResult> User(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await userServices.InsertUser(user);
                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Add Success" },
                        Data = user
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
                    Data = user
                });
            }
        }

        [HttpPut("User")]
        public async Task<IActionResult> PutUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDb = await userServices.GetUserAsync(user.Id);

                    userDb.FristName = user.FristName;
                    userDb.LastName = user.LastName;
                    userDb.Email = user.Email;
                    userDb.IdentityCard = user.IdentityCard;
                    userDb.Sex = user.Sex;


                    await userServices.UpdateUser(userDb);

                    return Ok(new ResultApi
                    {
                        Status = 200,
                        Success = true,
                        Message = new[] { "Edit success" },
                        Data = userDb
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

        [HttpDelete("User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await userServices.DeleteUser(id);
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