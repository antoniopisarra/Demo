using Audit.EntityFramework;
using Demo.Model;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess;

public class DemoDbContext(DbContextOptions<DemoDbContext> options) : AuditDbContext(options)
{
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoDbContext).Assembly);
    }
}