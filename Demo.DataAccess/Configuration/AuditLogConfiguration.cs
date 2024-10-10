using Demo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Configuration;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(log => log.Id);
        builder.Property(log => log.TipoEvento).IsRequired().HasMaxLength(50);
        builder.Property(log => log.NomeTabella).IsRequired().HasMaxLength(100);
        builder.Property(log => log.ChiavePrimaria).IsRequired(false);
        builder.Property(log => log.ValoriPrecedenti).IsRequired(false);
        builder.Property(log => log.NuoviValori).IsRequired(false);
        builder.Property(log => log.Utente).IsRequired(false);

    }
}