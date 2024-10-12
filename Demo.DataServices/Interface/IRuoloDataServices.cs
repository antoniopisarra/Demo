using Demo.Model;
using Demo.ModelDto.Ruolo;

namespace Demo.DataServices.Interface;

public interface IRuoloDataServices
{
    Task AggiungiNuovoRuoloAsync(Ruolo ruolo);
    Task ModificaRuoloAsync(Ruolo ruoloModificato);
    Task<List<Ruolo>> OttieniElencoRuoliAsync();
    Task<Ruolo> OttieniRuoloByTipoAsync(string tipoRuolo);
    Task<List<RuoloDto>> OttieniElencoRuoliDtoAsync();
}