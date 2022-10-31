using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Common.Models
{
    public static class JwtSettingsConfig
    {
        public static string AccessTokenTime { get; set; } = "";
        public static string RefreshTokenTime { get; set; } = "";
        public static string SecretKey { get; set; } = "";
        public static void ConfigurationJwtSettings(IConfiguration configuration)
        {
            AccessTokenTime=configuration["JwtSettings:AccessTokenTime"];
            RefreshTokenTime= configuration["JwtSettings:RefreshTokenTime"];
            SecretKey= configuration["JwtSettings:SecretKey"];
        }
    }
}
