using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IJwtServices
    {
        Task<ResultApi> getTokenAsync(LoginRequest loginRequest,string IPAdress);
        Task<ResultApi> getRefreshTokenAsync(int UserId,string IPAdress,string roleName);
        ResultApi checkValidate(RefreshTokenRequest refreshTokenRequest);
        Task<ResultApi> RevokeRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> getRefreshTokenDbAsync(string accessToken);
        bool isTokenLive(string accessToken);
    }
}
