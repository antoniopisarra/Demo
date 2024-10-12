using Demo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Configuration;

public class UtenteRuoloConfiguration : IEntityTypeConfiguration<UtenteRuolo>
{
    public void Configure(EntityTypeBuilder<UtenteRuolo> builder)
    {
        builder.HasKey(ruolo => ruolo.Id);
        builder.HasOne(r => r.Utente).WithMany(u => u.Ruoli).HasForeignKey(r => r.IdUtente);
        builder.HasOne(r => r.Ruolo).WithMany(r => r.Utenti).HasForeignKey(r => r.IdRuolo);
    }
}