using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SklepGrovly;
using SklepGrovly.Exceptions;
using SklepGrovly.Services.Authorization;
using SklepGrovly.Services.Categories;
using SklepGrovly.Services.Products;

var builder = WebApplication.CreateBuilder(args);

//JWT
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey))
            };
        });



builder.Services.AddAuthorization();


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategorieService, CategorieService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Rejestrujemy DbContext w kontenerze DI.
// AddDbContext domyślnie używa cyklu życia Scoped.

builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"));

});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
    app.MapOpenApi();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "My API V1"));

    
}

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();
app.MapControllers();
app.Run();