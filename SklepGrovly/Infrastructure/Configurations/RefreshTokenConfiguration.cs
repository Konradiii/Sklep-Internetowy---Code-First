using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class RefreshTokenConfiguration
{
    public class ProduktConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.CreatedAt).IsRequired();
            builder.Property(r => r.ExpiresAt).IsRequired();
            
            builder.HasOne(r=> r.Osoba)
                .WithMany(r=>r.RefreshTokens)
                .HasForeignKey(r=>r.Id_Osoba)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}