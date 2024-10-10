using Demo.DataAccess;
using Demo.DataServices.Interface;
using Demo.Model.Utente;

namespace Demo.DataServices.Implementation;

public class UtenteDataServices(DemoDbContext dbContext) : IUtenteDataServices
{
    public async Task AggiungiNuovoUtenteAsync(Utente utente)
    {
        await dbContext.AddAsync(utente);
        await dbContext.SaveChangesAsync();
    }
}