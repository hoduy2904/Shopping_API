using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoppingAPI.REPO;
using ShoppingAPI.REPO.Repository;
using ShoppingAPI.Services.Interfaces;
using ShoppingAPI.Services.Services;
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

app.MapControllers();

app.Run();
