using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingAPI.Common;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO;
using ShoppingAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingAPI.Services.Services
{
    public class JwtServices : IJwtServices
    {
        private readonly ShoppingContext shoppingContext;

        private readonly IConfiguration configuration;
        public JwtServices(ShoppingContext shoppingContext, IConfiguration  configuration)
        {
            this.shoppingContext = shoppingContext;
            this.configuration = configuration;
        }
        public async Task<ResultApi> getTokenAsync(LoginRequest loginRequest)
        {
            string PasswordHasing = StringHashing.Hash(loginRequest.Password);
            var user = await shoppingContext.Users.SingleOrDefaultAsync(x => x.Username.Equals(loginRequest.Username) && x.PasswordHash.Equals(PasswordHasing));
            if (user != null)
            {
                var accessToken = GenarateToken(user.Id, "", user.UserRoles.FirstOrDefault().Role.Name);
                var refreshToken = GenarateRefreshToken();
                RefreshTokenRequest refresh = new RefreshTokenRequest();
                refresh.RefreshToken = refreshToken;
                refresh.AccessToken = accessToken;
                return await SaveTokenDetails(accessToken,refreshToken,user.Id);
            }
            return new ResultApi
            {
                Success=false,
                Message="Incorrect Username or Password"
            };
        }

        private async Task<ResultApi> SaveTokenDetails(string accessToken,string refreshToken,int UserId)
        {
            var refreshDb=new RefreshToken
            {
                
                Expired = DateTime.UtcNow.AddDays(2),
                Refresh = refreshToken,
                Token = accessToken,
                UserId = UserId
            };

            shoppingContext.Add(refreshDb);
            await shoppingContext.SaveChangesAsync();
            return new ResultApi
            {
                Success = true,
                Data = new RefreshTokenRequest
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }

        private string GenarateRefreshToken()
        {
            var bytes = new byte[64];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
                return rng.ToString();
            }
        }

        private string GenarateToken(int UserId,string IpAdress, string RoleName)
        {
            //Get Secret and get bytes secret
            var secret = configuration["JwtSettings:SecretKey"];
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            //TokenSecurityHandle
            var tokenSecurityHandle = new JwtSecurityTokenHandler();
            //Claims
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId,UserId.ToString()),
                new Claim(ClaimTypes.Role,RoleName)
            });
            //JwtSecurityDecriptor
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretBytes), SecurityAlgorithms.HmacSha256)
            };
            //Write Token
            var token = tokenSecurityHandle.CreateToken(tokenDescriptor);
            return tokenSecurityHandle.WriteToken(token);
        }
        public async Task<ResultApi> getRefreshTokenAsync(int UserId,string IPAdress,string roleName)
        {
            var accessToken = GenarateToken(UserId, IPAdress, roleName);
            var refreshToken = GenarateRefreshToken();
            return await SaveTokenDetails(accessToken, refreshToken, UserId);
        }
    }
}
