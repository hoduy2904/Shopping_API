using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.REPO;
using ShoppingAPI.Services.Interfaces;
using ShoppingAPI.Services.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Common.Config;

namespace ShoppingAPI
{
    public class Startup
    {
        public IConfigurationRoot configurationRoot { get; set; }
        public Startup(IConfigurationRoot configurationRoot)
        {
            this.configurationRoot = configurationRoot;
        }
        public void ConfigureServices(IServiceCollection Services, IConfiguration Configuration)
        {
            //Start config file
            JwtSettingsConfig.ConfigurationJwtSettings(Configuration);
            PagingSettingsConfig.ConfigurationPagingSettings(Configuration);
            SaveFileConfig.ConfigurationSaveFileSettings(Configuration);

            Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            Services.AddDbContext<ShoppingContext>(options =>
            {
                options
                .UseSqlServer(Configuration.GetConnectionString("ShoppingContext"));
            });

            Services.AddControllersWithViews().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Services.AddScoped<ICartServices, CartServices>();
            Services.AddScoped<ICategoryServices, CategoryServices>();
            Services.AddScoped<IShoppingDeliveryAddressServices, ShoppingDeliveryAddressServices>();
            Services.AddScoped<IProductImageServices, ProductImageServices>();
            Services.AddScoped<IProductServices, ProductServices>();
            Services.AddScoped<IProductVariationServices, ProductVariationServices>();
            Services.AddScoped<IUserServices, UserServices>();
            Services.AddScoped<IRoleServices, RoleServices>();
            Services.AddScoped<IUserRoleServices, UserRoleServices>();
            Services.AddScoped<IJwtServices, JwtServices>();
            Services.AddScoped<IInvoiceServices, InvoiceServices>();
            Services.AddScoped<IInvoiceDetailsServices, InvoiceDetailsServices>();
            Services.AddScoped<IProductRatingServices, ProductRatingServices>();
            Services.AddScoped<IProductRatingImageServices, ProductRatingImageServices>();

            //Jwt
            //Get Secret Key and Bytes
            var secretKey = Configuration["JwtSettings:SecretKey"];
            var secretBytes = Encoding.UTF8.GetBytes(secretKey);
            //Setting Tokenvalidatetion Parameter
            var tokenParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretBytes),

                ClockSkew = TimeSpan.Zero
            };

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenParameters;
                options.Events = new JwtBearerEvents();
                options.Events.OnTokenValidated = async (context) =>
                {
                    var jwtServer = context.HttpContext.RequestServices.GetService<IJwtServices>();
                    var jwtToken = context.SecurityToken as JwtSecurityToken;
                    if (!jwtServer.isTokenLive(jwtToken.RawData))
                    {
                        context.HttpContext.Response.Headers.Remove("Authorization");
                        context.Fail("Invalid Token");
                    }
                };
            });

            //Config Swagger
            Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }
    }
}
