using Microsoft.EntityFrameworkCore;
using SklepGrovly;

var builder = WebApplication.CreateBuilder(args);


// Rejestrujemy DbContext w kontenerze DI.
// AddDbContext domyślnie używa cyklu życia Scoped.
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();