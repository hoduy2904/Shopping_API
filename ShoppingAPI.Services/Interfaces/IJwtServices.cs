using ShoppingAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Services.Interfaces
{
    public interface IJwtServices
    {
        Task<ResultApi> getTokenAsync(LoginRequest loginRequest);
        Task<ResultApi> getRefreshTokenAsync(int UserId,string IPAdress,string roleName);
    }
}
