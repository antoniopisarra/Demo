using Demo.Model.Utente;

namespace Demo.DataServices.Interface;

public interface IUtenteDataServices
{
    Task AggiungiNuovoUtenteAsync(Utente utente);
    Task AggiornaUtenteAsync(Utente utenteModificato);
    Task<bool> VerificaEsistenzaNomeUtenteAsync(string username);
}