using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.Data.Models;
using ShoppingAPI.Services.Interfaces;

namespace ShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtServices jwtServices;
        public AuthController(IJwtServices jwtServices)
        {
            this.jwtServices = jwtServices;
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
        [HttpPost("[Action]")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var remoteIP = HttpContext.Connection.RemoteIpAddress == null ? "" : HttpContext.Connection.RemoteIpAddress.ToString();

                var result = jwtServices.checkValidate(refreshTokenRequest);
                if (!result.Success)
                    return BadRequest(result);
                var isRevoked = await jwtServices.RevokeRefreshToken((RefreshToken)result.Data);
                if (!isRevoked.Success)
                    return BadRequest(isRevoked);
                var RefreshTokenDb = (result.Data as RefreshToken);
                var response = await jwtServices.getRefreshTokenAsync(RefreshTokenDb.UserId,
                    remoteIP,
                    RefreshTokenDb.User.UserRoles.FirstOrDefault().Role.Name);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultApi
                {
                    Success = false,
                    Message = new[] { ex.Message }
                });
            }
        }

    }
}
