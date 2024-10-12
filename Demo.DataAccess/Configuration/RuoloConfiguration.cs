using Demo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Configuration;

public class RuoloConfiguration : IEntityTypeConfiguration<Ruolo>
{
    public void Configure(EntityTypeBuilder<Ruolo> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => r.TipoRuolo).IsUnique();
        builder.Property(r => r.TipoRuolo).IsRequired().HasMaxLength(50);
        builder.Property(r => r.Descrizione).IsRequired(false).HasMaxLength(300);
    }
}