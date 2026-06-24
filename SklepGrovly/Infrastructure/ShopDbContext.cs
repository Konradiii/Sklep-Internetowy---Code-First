using Microsoft.EntityFrameworkCore;

namespace SklepGrovly;

//Server=(localdb)\MSSQLLocalDB;Database=Shop_Database

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);
    }
    
}