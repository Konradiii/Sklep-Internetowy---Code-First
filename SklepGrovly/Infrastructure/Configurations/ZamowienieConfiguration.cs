using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class ZamowienieConfiguration : IEntityTypeConfiguration<Zamowienie>
{
    public void Configure(EntityTypeBuilder<Zamowienie> builder)
    {
        builder.HasKey(e => e.Id_Zamowienie);
        
        builder.Property(p=> p.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(p => p.Klient)
            .WithMany(p => p.Zamowienia)
            .HasForeignKey(e => e.Id_Klient)
            .OnDelete(DeleteBehavior.Restrict);
        
        
    }
    
}