using Demo.DataAccess;
using Demo.DataServices.Interface;
using Demo.Model;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataServices.Implementation;

public class RuoloDataServices(DemoDbContext dbContext) : IRuoloDataServices
{
    public async Task AggiungiNuovoRuoloAsync(Ruolo ruolo)
    {
        await dbContext.Ruoli.AddAsync(ruolo);
        await dbContext.SaveChangesAsync();
    }

    public async Task ModificaRuoloAsync(Ruolo ruoloModificato)
    {
        var ruolo = await dbContext.Ruoli.FirstAsync(r => r.Id == ruoloModificato.Id);

        ruolo.Descrizione = ruoloModificato.Descrizione;
        ruolo.TipoRuolo = ruoloModificato.TipoRuolo;

        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Ruolo>> OttieniElencoRuoliAsync() =>
        await dbContext.Ruoli.AsNoTracking().ToListAsync();

    public async Task<Ruolo> OttieniRuoloByTipoAsync(string tipoRuolo) =>
        await dbContext.Ruoli.AsNoTracking().FirstAsync(ruolo => ruolo.TipoRuolo == tipoRuolo);
}