using Microsoft.EntityFrameworkCore;
using SklepGrovly;

var builder = WebApplication.CreateBuilder(args);

// Pobieramy connection string z appsettings.json
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

// Rejestrujemy DbContext w kontenerze DI.
// AddDbContext domyślnie używa cyklu życia Scoped.
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();