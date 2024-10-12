using Demo.Model;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess;

public class DemoDbContext(DbContextOptions<DemoDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.AddInterceptors(new AuditInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoDbContext).Assembly);
    }

    //Tabelle Database

    public DbSet<AuditLog> AuditLogs { get; set; }

    public DbSet<Utente> Utenti { get; set; }

    public DbSet<Ruolo> Ruoli { get; set; }

    public DbSet<UtenteRuolo> UtentiRuoli { get; set; }
}