using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class OpiniaConfiguration : IEntityTypeConfiguration<Opinia>
{
    public void Configure(EntityTypeBuilder<Opinia> builder)
    {
        builder.HasKey(o => o.Id_Opinia);

        builder.Property(o => o.Tresc)
            .HasMaxLength(1000);

        // relacja do klienta — Restrict (chronimy opinie, nie kasujemy ich z klientem)
        builder.HasOne(o => o.Klient)
            .WithMany(k => k.Opinie)
            .HasForeignKey(o => o.Id_Klient)
            .OnDelete(DeleteBehavior.Restrict);

        // relacja do produktu — Restrict (i unikamy multiple cascade paths)
        builder.HasOne(o => o.Produkt)
            .WithMany(p => p.Opinie)
            .HasForeignKey(o => o.Id_Produkt)
            .OnDelete(DeleteBehavior.Restrict);

        // jeden klient = jedna opinia na produkt
        builder.HasIndex(o => new { o.Id_Klient, o.Id_Produkt })
            .IsUnique();
    }
}