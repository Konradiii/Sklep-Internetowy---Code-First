using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class PozycjaWZamowieniuConfiguration : IEntityTypeConfiguration<PozycjaWZamowieniu>
{
    public void Configure(EntityTypeBuilder<PozycjaWZamowieniu> builder)
    {
        builder.HasKey(p => p.Id_Pozycja_Zamowienie);

        builder.Property(p => p.CenaZakupu)
            .HasColumnType("decimal(10,2)");

        // relacja do zamówienia — pozycje giną z zamówieniem
        builder.HasOne(p => p.Zamowienie)
            .WithMany(z => z.PozycjaWZamowieniu)
            .HasForeignKey(p => p.Id_Zamowienie)
            .OnDelete(DeleteBehavior.Cascade);

        // relacja do produktu — Restrict, żeby chronić historię i uniknąć multiple cascade paths
        builder.HasOne(p => p.Produkt)
            .WithMany(pr => pr.PozycjaWZamowieniu)
            .HasForeignKey(p => p.Id_Produkt)
            .OnDelete(DeleteBehavior.Restrict);
    }
}