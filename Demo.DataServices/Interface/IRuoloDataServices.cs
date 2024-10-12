using Demo.Model;

namespace Demo.DataServices.Interface;

public interface IRuoloDataServices
{
    Task AggiungiNuovoRuoloAsync(Ruolo ruolo);
    Task ModificaRuoloAsync(Ruolo ruoloModificato);
    Task<List<Ruolo>> OttieniElencoRuoliAsync();
    Task<Ruolo> OttieniRuoloByTipoAsync(string tipoRuolo);
}