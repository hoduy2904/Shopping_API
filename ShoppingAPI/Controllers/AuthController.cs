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
        //Register by User
        [HttpPost("[Action]")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                var user = await userServices.FindByUsername(registerRequest.Username);
                //check user exists
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

                //If user exists
                return BadRequest(new ResultApi
                {
                    Message = new[] { "Username is Exists" },
                    Success = false,
                    Status = BadRequest().StatusCode
                });
            }

            return BadRequest();
        }


        //login
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

        //Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            //Get accessToken
            var AccessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer", "");
            var refreshToken = await jwtServices.getRefreshTokenDbAsync(AccessToken);

            //Check if refreshToken is null
            if (refreshToken == null)
                return Unauthorized();

            //If not null then remove authorization from Header
            Response.Headers.Remove("Authorization");
            //Revoke Token and refreshToken
            await jwtServices.RevokeRefreshToken(refreshToken);

            return Ok(new ResultApi
            {
                Success = true,
                Message = new[] { "Logout success " },
                Status = (int)HttpStatusCode.OK
            });
        }


        //RefreshToken
        [HttpPost("[Action]")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            //Get IP Address
            var remoteIP = HttpContext.Connection.RemoteIpAddress == null ? "" : HttpContext.Connection.RemoteIpAddress.ToString();

            //Check RefreshToken is vaild
            var result = jwtServices.checkValidate(refreshTokenRequest);

            if (!result.Success)
                return BadRequest(result);
            //Check RefreshToken was Revoked
            var isRevoked = await jwtServices.RevokeRefreshToken((RefreshToken)result.Data);
            if (!isRevoked.Success)
                return BadRequest(isRevoked);

            var RefreshTokenDb = (result.Data as RefreshToken);

            //Get UserRole
            var userRole = RefreshTokenDb.User.UserRoles?.FirstOrDefault();

            string roleName = "User";
            //Check if null userole then update, if not then roleName is User
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
