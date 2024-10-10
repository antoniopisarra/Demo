using Demo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Demo.DataAccess;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly List<AuditLog> _auditLogs = [];
    private string GetPrimaryKey(EntityEntry entry)
    {
        var key = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
        return key != null ? key.CurrentValue.ToString() : "N/A";
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var audit = eventData.Context.ChangeTracker.Entries()
            .Where(x => x.Entity is not AuditLog && x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(x => new AuditLog
            {
                TipoEvento = x.State.ToString(),
                DataModifica = DateTime.Now,
                ChiavePrimaria = GetPrimaryKey(x),
                NomeTabella = eventData.Context.Model.FindEntityType(x.Entity.GetType())?.GetTableName(),
                ValoriPrecedenti = x.OriginalValues.ToString(),
                NuoviValori = x.CurrentValues.ToString()
            });

        if (!audit.Any())
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        _auditLogs.AddRange(audit);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        foreach (var log in _auditLogs)
        {
            log.Successo = true;
        }

        if (_auditLogs.Count > 0)
        {
            eventData.Context.Set<AuditLog>().AddRange(_auditLogs);
            _auditLogs.Clear();
            await eventData.Context.SaveChangesAsync();
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}