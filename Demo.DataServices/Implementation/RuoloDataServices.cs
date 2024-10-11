using Demo.DataAccess;
using Demo.DataServices.Interface;
using Demo.Model.Utente;
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



}