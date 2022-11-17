using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Common.Config
{
    public static class PagingSettingsConfig
    {
        public static int pageDefault { get; set; }
        public static int pageSize { get; set; }
        public static void ConfigurationPagingSettings(IConfiguration configuration)
        {
            pageDefault = int.Parse(configuration["PagingSettings:pageDefault"]);
            pageSize = int.Parse(configuration["PagingSettings:pageSize"]);
        }
    }
}
