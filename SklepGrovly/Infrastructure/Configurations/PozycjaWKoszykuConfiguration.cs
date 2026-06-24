using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class PozycjaWKoszykuConfiguration : IEntityTypeConfiguration<PozycjaWKoszyku>
{
    public void Configure(EntityTypeBuilder<PozycjaWKoszyku> builder)
    {
        builder.HasKey(p => p.Id_Pozycja_Koszyk);

        builder.Ignore(p => p.CenaPozycji);   // pochodna — nie zapisujemy w bazie

        builder.HasOne(p => p.Koszyk)
            .WithMany(k => k.PozycjeWKoszyku)
            .HasForeignKey(p => p.Id_Koszyk)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Produkt)
            .WithMany(pr => pr.PozycjaWKoszyku)
            .HasForeignKey(p => p.Id_Produkt)
            .OnDelete(DeleteBehavior.Restrict);
    }
}