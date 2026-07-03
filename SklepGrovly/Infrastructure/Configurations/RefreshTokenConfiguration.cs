using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;


    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.TokenHash).IsRequired().HasMaxLength(64);
            builder.Property(r => r.CreatedAt).IsRequired();
            builder.Property(r => r.ExpiresAt).IsRequired();

            builder.HasIndex(r => r.TokenHash).IsUnique();

            builder.HasOne(r => r.Osoba)
                .WithMany(r => r.RefreshTokens)
                .HasForeignKey(r => r.Id_Osoba)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
