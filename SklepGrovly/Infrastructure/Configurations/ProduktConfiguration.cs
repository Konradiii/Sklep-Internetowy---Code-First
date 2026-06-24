using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class ProduktConfiguration : IEntityTypeConfiguration<Produkt>
{
    public void Configure(EntityTypeBuilder<Produkt> builder)
    {
        builder.HasKey(p=>p.Id_Produkt);
        
        builder.Property(p => p.Nazwa)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p=>p.Cena)
            .HasColumnType("decimal(10,2)");
        
        builder.Property(p=>p.Znizka)
            .HasColumnType("decimal(10,2)");

        builder.HasOne(p => p.Kategoria)
            .WithMany(k => k.Produkty)
            .HasForeignKey(k => k.Id_Kategoria)
            .OnDelete(DeleteBehavior.Restrict);

    }
    
}
