using Demo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;

namespace Demo.DataAccess;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly List<AuditLog> _auditLogs = [];

    private string GetPrimaryKey(EntityEntry entry)
    {
        if (entry.State == EntityState.Added)
        {
            return 0.ToString();
        }
        var key = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
        return key != null ? key.CurrentValue.ToString() : "N/A";
    }

    private Dictionary<string, object> OttieniValoriPrecedenti(EntityEntry entry)
    {
        var valori = new Dictionary<string, object>();
        foreach (var property in entry.Properties)
        {
            if (property.IsModified)
            {
                valori.Add(property.Metadata.Name, property.OriginalValue);
            }
        }
        return valori;
    }

    private Dictionary<string, object> OttieniNuoviValori(EntityEntry entry)
    {
        var valori = new Dictionary<string, object>();
        foreach (var property in entry.Properties)
        {
            valori.Add(property.Metadata.Name, property.CurrentValue);
        }
        return valori;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
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
                ValoriPrecedenti = JsonSerializer.Serialize(OttieniValoriPrecedenti(x)),
                NuoviValori = JsonSerializer.Serialize(OttieniNuoviValori(x))
            });

        if (!audit.Any())
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        _auditLogs.AddRange(audit);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new())
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