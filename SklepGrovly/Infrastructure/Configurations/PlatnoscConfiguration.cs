using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class PlatnoscConfiguration : IEntityTypeConfiguration<Platnosc>
{
    public void Configure(EntityTypeBuilder<Platnosc> builder)
    {
        builder.HasKey(e => e.Id_Platnosc);
        
        builder.Property(p=>p.KwotaPlatnosci)
            .HasColumnType("decimal(10,2)");
        
        builder.Property(p=>p.MetodaPlatnosci)
            .HasConversion<string>()
            .HasMaxLength(50);
        
        builder.Property(p=> p.StatusPlatnosci)
            .HasConversion<string>()
            .HasMaxLength(50);
        
        builder.Property(p=>p.IdZBramkiPlatniczej)
            .HasMaxLength(150);
        
        //Relacja 1:1 Z zamowieniem
        builder.HasOne(p => p.Zamowienie)
            .WithOne(p => p.Platnosc)
            .HasForeignKey<Platnosc>(p => p.Id_Zamowienie);
        
    }
    
}