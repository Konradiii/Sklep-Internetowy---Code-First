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
        
        
        builder.Property(p => p.ImieOdbiorcy)
            .HasMaxLength(50);
        
        builder.Property(p => p.NazwiskoOdbiorcy)
            .HasMaxLength(50);
        
        builder.Property(p=> p.Ulica)
            .HasMaxLength(100);
            
        builder.Property(p=> p.NrDomu)
            .HasMaxLength(100);
        
        builder.Property(p=> p.KodPocztowy)
            .HasMaxLength(6);
        
        builder.Property(p=> p.Miejscowosc)
            .HasMaxLength(100);
            
        builder.Property(p=> p.TelefonOdbiorcy)
            .HasMaxLength(20);

    
        

        builder.HasOne(p => p.Klient)
            .WithMany(p => p.Zamowienia)
            .HasForeignKey(e => e.Id_Klient)
            .OnDelete(DeleteBehavior.Restrict);
        
        
    }
    
}