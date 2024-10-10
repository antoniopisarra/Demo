using Demo.Model;
using Demo.Model.Utente;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess;

public class DemoDbContext(DbContextOptions<DemoDbContext> options) : DbContext(options)
{
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Utente> Utenti { get; set; }
    public DbSet<Ruolo> Ruoli { get; set; }
    public DbSet<UtenteRuolo> UtentiRuoli { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoDbContext).Assembly);
    }
}