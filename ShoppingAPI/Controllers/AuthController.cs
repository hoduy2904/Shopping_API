using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ShoppingAPI.Services.Interfaces;
using ShoppingAPI.Common;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Common.Models;
using System.Net;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtServices jwtServices;
        private readonly IRoleServices roleServices;
        private readonly IUserServices userServices;
        public AuthController(IJwtServices jwtServices, IRoleServices roleServices, IUserServices userServices)
        {
            this.jwtServices = jwtServices;
            this.roleServices = roleServices;
            this.userServices = userServices;
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                var user = await userServices.FindByUsername(registerRequest.Username);
                if (user == null)
                {
                    var role = await roleServices.FindRoleByname("User");
                    var UserRole = new List<UserRole>();

                    var us = new User
                    {
                        Email = registerRequest.Email,
                        FristName = registerRequest.FirstName,
                        LastName = registerRequest.LastName,
                        PasswordHash = StringHashing.Hash(registerRequest.Password),
                        Sex = registerRequest.Sex,
                        Username = registerRequest.Username,
                        IdentityCard = registerRequest.IdentityCard
                    };

                    UserRole.Add(new UserRole
                    {
                        RoleId = role.Id,
                        UserId = us.Id
                    });

                    us.UserRoles = UserRole;
                    await userServices.InsertUser(us);

                    return Ok(new ResultApi
                    {
                        Message = new[] { "Register Successfull" },
                        Status = (int)HttpStatusCode.OK,
                        Success = true
                    });
                }

                return BadRequest(new ResultApi
                {
                    Message = new[] { "Username is Exists" },
                    Success = false,
                    Status = BadRequest().StatusCode
                });
            }
            return BadRequest();
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var remoteIP = HttpContext.Connection.RemoteIpAddress == null ? "" : HttpContext.Connection.RemoteIpAddress.ToString();
                var resultApi = await jwtServices.getTokenAsync(loginRequest, remoteIP);
                return Ok(resultApi);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var AccessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer", "");
            var refreshToken = await jwtServices.getRefreshTokenDbAsync(AccessToken);
            if (refreshToken == null)
                return Unauthorized();

            Response.Headers.Remove("Authorization");
            await jwtServices.RevokeRefreshToken(refreshToken);

            return Ok(new ResultApi
            {
                Success = true,
                Message = new[] { "Logout success " },
                Status = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var remoteIP = HttpContext.Connection.RemoteIpAddress == null ? "" : HttpContext.Connection.RemoteIpAddress.ToString();

            var result = jwtServices.checkValidate(refreshTokenRequest);
            if (!result.Success)
                return BadRequest(result);

            var isRevoked = await jwtServices.RevokeRefreshToken((RefreshToken)result.Data);
            if (!isRevoked.Success)
                return BadRequest(isRevoked);

            var RefreshTokenDb = (result.Data as RefreshToken);

            var userRole = RefreshTokenDb.User.UserRoles?.FirstOrDefault();
            string roleName = "User";
            if (userRole != null)
            {
                var Role = userRole.Role;
                if (Role != null)
                    roleName = Role.Name;
            }

            var response = await jwtServices.getRefreshTokenAsync(RefreshTokenDb.UserId,
                remoteIP,
                roleName);

            return Ok(response);
        }

    }
}
