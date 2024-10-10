using Demo.Model.Utente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Configuration;

public class UtenteConfiguration : IEntityTypeConfiguration<Utente>
{
    public void Configure(EntityTypeBuilder<Utente> builder)
    {
        builder.HasKey(utente => utente.Id);
        builder.HasIndex(utente => utente.Username).IsUnique();
        builder.Property(utente => utente.Username).IsRequired().HasMaxLength(100);
        builder.Property(utente => utente.PasswordHash).IsRequired().HasMaxLength(60);
    }
}