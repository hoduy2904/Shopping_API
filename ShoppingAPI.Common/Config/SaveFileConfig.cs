using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Common.Config
{
    public static class SaveFileConfig
    {
        public static string? Image { get; set; }
        public static string? AllFile { get; set; }
        public static void ConfigurationSaveFileSettings(IConfiguration configuration)
        {
            Image = configuration["SavefileSettings:Image"];
            AllFile = configuration["SavefileSettings:AllFile"];
        }
    }
}
