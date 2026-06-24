using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class KoszykConfiguration : IEntityTypeConfiguration<Koszyk>
{
    public void Configure(EntityTypeBuilder<Koszyk> builder)
    {
        builder.HasKey(k => k.Id_Koszyk);

        builder.HasOne(k => k.Klient)
            .WithOne(kl => kl.Koszyk)
            .HasForeignKey<Koszyk>(k => k.Id_Klient)
            .OnDelete(DeleteBehavior.Cascade);
    }
}