using Demo.DataAccess;
using Demo.DataServices.Interface;
using Demo.Model.Utente;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataServices.Implementation;

public class UtenteDataServices(DemoDbContext dbContext) : IUtenteDataServices
{

    public async Task<bool> VerificaEsistenzaNomeUtenteAsync(string username)
    {
        return await dbContext.Utenti.AnyAsync(u => u.Username == username);
    }

    public async Task AggiungiNuovoUtenteAsync(Utente utente)
    {
        await dbContext.AddAsync(utente);
        await dbContext.SaveChangesAsync();
    }

    public async Task AggiornaUtenteAsync(Utente utenteModificato)
    {
        var vecchioUtente = await dbContext.Utenti.FindAsync(utenteModificato.Id);

        vecchioUtente.PasswordHash = utenteModificato.PasswordHash;
        vecchioUtente.Username =   utenteModificato.Username;
        await dbContext.SaveChangesAsync();
    }
}