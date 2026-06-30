using Microsoft.EntityFrameworkCore;
using SklepGrovly;
using SklepGrovly.Exceptions;
using SklepGrovly.Services.Categories;
using SklepGrovly.Services.Products;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategorieService, CategorieService>();


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

app.UseExceptionHandler();
app.MapControllers();
app.Run();