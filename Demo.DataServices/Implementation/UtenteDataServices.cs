using Demo.DataAccess;
using Demo.DataServices.Interface;
using Demo.Model.Utente;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataServices.Implementation;

public class UtenteDataServices(DemoDbContext dbContext) : IUtenteDataServices
{
    public async Task<bool> VerificaEsistenzaNomeUtenteAsync(string username) =>
        await dbContext.Utenti.AsNoTracking().AnyAsync(u => u.Username == username);

    public async Task<Utente?> OttieniUtenteByUsernameAsync(string username) =>
        await dbContext.Utenti.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);

    public async Task AggiungiNuovoUtenteAsync(Utente utente)
    {
        await dbContext.AddAsync(utente);
        await dbContext.SaveChangesAsync();
    }

    public async Task AggiornaUtenteAsync(Utente utenteModificato)
    {
        var utente = await dbContext.Utenti.FindAsync(utenteModificato.Id);

        if (utente != null)
        {
            utente.PasswordHash = utenteModificato.PasswordHash;
            utente.Username = utenteModificato.Username;
        }

        await dbContext.SaveChangesAsync();
    }
}