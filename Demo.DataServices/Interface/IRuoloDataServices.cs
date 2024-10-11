using Demo.Model.Utente;

namespace Demo.DataServices.Interface;

public interface IRuoloDataServices
{
    Task AggiungiNuovoRuoloAsync(Ruolo ruolo);
    Task ModificaRuoloAsync(Ruolo ruoloModificato);
}