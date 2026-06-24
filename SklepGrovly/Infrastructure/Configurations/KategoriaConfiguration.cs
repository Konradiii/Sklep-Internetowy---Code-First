using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SklepGrovly.Entities;

namespace SklepGrovly.Configurations;

public class KategoriaConfiguration : IEntityTypeConfiguration<Kategoria>
{
    public void Configure(EntityTypeBuilder<Kategoria> builder)
    {
        builder.HasKey(k => k.Id_Kategoria);
        
        builder.Property(k=>k.Nazwa)
            .HasMaxLength(200)
            .IsRequired();
    }
    
}