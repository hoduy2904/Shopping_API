using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingAPI.Common;
using ShoppingAPI.Common.Config;
using ShoppingAPI.Common.Models;
using ShoppingAPI.Data.Models;
using ShoppingAPI.REPO;
using ShoppingAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingAPI.Services.Services
{
    public class JwtServices : IJwtServices
    {
        private readonly ShoppingContext shoppingContext;

        private readonly IConfiguration configuration;
        public JwtServices(ShoppingContext shoppingContext, IConfiguration configuration)
        {
            this.shoppingContext = shoppingContext;
            this.configuration = configuration;
        }

        public async Task<ResponseApi> getTokenAsync(LoginRequest loginRequest, string IPAdress)
        {
            string PasswordHasing = StringHashing.Hash(loginRequest.Password);
            var user = await shoppingContext.Users.Include(ur => ur.UserRoles)
                .ThenInclude(r => r.Role)
                .SingleOrDefaultAsync(x =>
            x.Username.Equals(loginRequest.Username)
            && x.PasswordHash.Equals(PasswordHasing)
            );

            if (user != null)
            {
                var accessToken = GenarateToken(user.Id, "", user.UserRoles.FirstOrDefault().Role.Name);
                var refreshToken = GenarateRefreshToken();
                RefreshTokenRequest refresh = new RefreshTokenRequest();
                refresh.RefreshToken = refreshToken;
                refresh.AccessToken = accessToken;
                return await SaveTokenDetails(accessToken, refreshToken, IPAdress, user.Id);
            }
            return new ResponseApi
            {
                Success = false,
                Message = new[] { "Incorrect Username or Password" }
            };
        }
        //Save Token Database
        private async Task<ResponseApi> SaveTokenDetails(string accessToken, string refreshToken, string IPAdress, int UserId)
        {
            var getInfomationToken = GetJwtSecurity(accessToken);

            int.TryParse(JwtSettingsConfig.RefreshTokenTime, out int RefreshTime);
            var refreshDb = new RefreshToken
            {
                TokenId = getInfomationToken.Id,
                Expired = DateTime.UtcNow.AddDays(RefreshTime),
                Refresh = refreshToken,
                Token = accessToken,
                UserId = UserId,
                IPAdress = IPAdress,
                Created = DateTime.UtcNow
            };

            shoppingContext.Add(refreshDb);
            await shoppingContext.SaveChangesAsync();
            return new ResponseApi
            {
                Success = true,
                Data = new
                {
                    accessToken,
                    acessToken_type="Bearer",
                    accessToken_time=getInfomationToken.ValidTo,
                }
            };
        }

        //Genarate Random RefreshToken
        private string GenarateRefreshToken()
        {
            var bytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        private string GenarateToken(int UserId, string IpAdress, string RoleName)
        {
            int.TryParse(JwtSettingsConfig.AccessTokenTime, out int AccessTime);
            //Get Secret and get bytes secret

            var secret = JwtSettingsConfig.SecretKey;

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
                Expires = DateTime.UtcNow.AddMinutes(AccessTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretBytes), SecurityAlgorithms.HmacSha256)
            };
            //Write Token
            var token = tokenSecurityHandle.CreateToken(tokenDescriptor);
            return tokenSecurityHandle.WriteToken(token);
        }

        public async Task<ResponseApi> getRefreshTokenAsync(int UserId, string IPAdress, string roleName)
        {
            var accessToken = GenarateToken(UserId, IPAdress, roleName);
            var refreshToken = GenarateRefreshToken();
            return await SaveTokenDetails(accessToken, refreshToken, IPAdress, UserId);
        }

        private JwtSecurityToken GetJwtSecurity(string accessToken)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            return tokenHandle.ReadJwtToken(accessToken);
        }

        public ResponseApi checkValidate(RefreshTokenRequest refreshTokenRequest)
        {
            var token = GetJwtSecurity(refreshTokenRequest.AccessToken);
            var refreshToken = shoppingContext.RefreshTokens.FirstOrDefault(x =>
            x.IsTrash == false
            && x.TokenId.Equals(token.Id)
            && x.Token.Equals(refreshTokenRequest.AccessToken)
            && x.Refresh.Equals(refreshTokenRequest.RefreshToken)
            );
            //Check Token Exists
            if (refreshToken == null)
                return new ResponseApi
                {
                    Success = false,
                    Message = new[] { "Refresh Token Not found" }
                };
            //Check RefreshToken expired
            if (refreshToken.IsExpired)
                return new ResponseApi
                {
                    Success = false,
                    Message = new[] { "Refresh Token Expired" }
                };
            //Check Alg Access Token
            if (!token.SignatureAlgorithm.Equals(SecurityAlgorithms.HmacSha256))
                return new ResponseApi
                {
                    Success = false,
                    Message = new[] { "Not match Alg" }
                };
            return new ResponseApi
            {
                Success = true,
                Data = refreshToken
            };
        }

        //Revoked RefreshToken
        public async Task<ResponseApi> RevokeRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                refreshToken.IsTrash = true;
                shoppingContext.RefreshTokens.Update(refreshToken);
                await shoppingContext.SaveChangesAsync();
                return new ResponseApi
                {
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseApi
                {
                    Success = false,
                    Message = new[] { ex.Message }
                };
            }
        }

        public async Task<RefreshToken> getRefreshTokenDbAsync(string accessToken)
        {
            return await shoppingContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(accessToken));
        }

        public bool isTokenLive(string accessToken)
        {
            var refreshToken = shoppingContext.RefreshTokens.FirstOrDefault(x =>
            x.Token.Equals(accessToken)
            && x.IsTrash == false
            );

            if (refreshToken != null)
                return true;
            return false;
        }
    }
}
