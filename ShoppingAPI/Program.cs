using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShoppingAPI.REPO;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using ShoppingAPI.Services.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShoppingContext>(options =>
{
    options.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("ShoppingContext"));
});

builder.Services.AddControllersWithViews().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IInfomationUserServices, InfomationUserServices>();
builder.Services.AddScoped<IProductImageServices, ProductImageServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IProductVariationServices, ProductVariationServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<IUserRoleServices, UserRoleServices>();
builder.Services.AddScoped<IJwtServices, JwtServices>();

//Jwt
//Get Secret Key and Bytes
var secretKey = builder.Configuration["JwtSettings:SecretKey"];
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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
builder.Services.AddSwaggerGen(opt =>
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});
app.MapControllers();

app.Run();
