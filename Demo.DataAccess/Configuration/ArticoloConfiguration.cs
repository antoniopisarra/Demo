using Demo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Configuration;

public class ArticoloConfiguration : IEntityTypeConfiguration<Articolo>
{
    public void Configure(EntityTypeBuilder<Articolo> builder)
    {
        builder.HasKey(articolo => articolo.Id);
        builder.Property(articolo => articolo.NomeArticolo).IsRequired().HasMaxLength(150);
    }
}