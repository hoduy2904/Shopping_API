using ShoppingAPI.Common.Models;
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
        Task<ResponseApi> getTokenAsync(LoginRequest loginRequest,string IPAdress);
        Task<ResponseApi> getRefreshTokenAsync(int UserId,string IPAdress,string roleName);
        ResponseApi checkValidate(RefreshTokenRequest refreshTokenRequest);
        Task<ResponseApi> RevokeRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> getRefreshTokenDbAsync(string accessToken);
        bool isTokenLive(string accessToken);
    }
}
