using Demo.Model;

namespace Demo.DataServices.Interface;

public interface IUtenteDataServices
{
    Task AggiungiNuovoUtenteAsync(Utente utente);
    Task AggiornaUtenteAsync(Utente utenteModificato);
    Task<bool> VerificaEsistenzaNomeUtenteAsync(string username);
    Task<Utente?> OttieniUtenteByUsernameAsync(string username);
}