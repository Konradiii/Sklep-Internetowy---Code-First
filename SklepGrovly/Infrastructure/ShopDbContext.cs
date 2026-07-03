using Microsoft.EntityFrameworkCore;
using SklepGrovly.Entities;

namespace SklepGrovly;

//Server=(localdb)\MSSQLLocalDB;Database=Shop_Database

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }
    
    public DbSet<Produkt> Produkt{ get; set; }
    public DbSet<Kategoria> Kategoria { get; set; }
    public DbSet<Osoba> Osoba { get; set; }
    public DbSet<Klient> Klienci { get; set; }
    public DbSet<Administrator> Administrator { get; set; }
    public DbSet<Koszyk> Koszyk { get; set; }
    public DbSet<PozycjaWKoszyku> PozycjeWKoszyku { get; set; }
    public DbSet<Zamowienie> Zamowienie { get; set; }
    public DbSet<PozycjaWZamowieniu> PozycjeWZamowieniu { get; set; }
    public DbSet<Platnosc> Platnosc { get; set; }
    public DbSet<Opinia> Opinia { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);
    }
    
}