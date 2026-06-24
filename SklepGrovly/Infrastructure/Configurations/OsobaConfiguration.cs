using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class OsobaConfiguration : IEntityTypeConfiguration<Osoba>
{
    public void Configure(EntityTypeBuilder<Osoba> builder)
    {
        builder.HasKey(e => e.Id_Osoba);
        
        builder.Property(o=>o.Imie)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e=> e.Nazwisko)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(o => o.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.Haslo)
            .IsRequired();

        builder.Property(o => o.NrTelefonu)
            .HasMaxLength(20);
        
        builder.Ignore(o => o.Wiek);
        
        builder.HasDiscriminator<string>("TypOsoby")
            .HasValue<Klient>("Klient")
            .HasValue<Administrator>("Administrator");
    }
    
}