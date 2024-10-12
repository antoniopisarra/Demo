using Demo.DataAccess;
using Demo.DataServices.Interface;
using Demo.Model;
using Demo.ModelDto.Ruolo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.DataServices.Implementation;

public class RuoloDataServices(DemoDbContext dbContext) : IRuoloDataServices
{
    #region Proiezioni

    private static readonly Expression<Func<Ruolo, RuoloDto>> ToRuoloDto = ruolo => new RuoloDto()
    {
        TipoRuolo = ruolo.TipoRuolo,
        Descrizione = ruolo.Descrizione,
        Id = ruolo.Id
    };

    #endregion Proiezioni

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

    public async Task<List<RuoloDto>> OttieniElencoRuoliDtoAsync() =>
        await dbContext.Ruoli.AsNoTracking().Select(ToRuoloDto).ToListAsync();

    public async Task<Ruolo> OttieniRuoloByTipoAsync(string tipoRuolo) =>
        await dbContext.Ruoli.AsNoTracking().FirstAsync(ruolo => ruolo.TipoRuolo == tipoRuolo);
}