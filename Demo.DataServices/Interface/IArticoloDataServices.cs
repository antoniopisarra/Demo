using Demo.Model;
using Demo.ModelDto.Articolo;

namespace Demo.DataServices.Interface;

public interface IArticoloDataServices
{
    Task<Articolo> AggiungiNuovoArticolo(Articolo articolo);
    Task<List<ArticoloDto>> OttieniElencoArticoliDtoAsync();
    Task EliminaArticoloByIdAsync(int id);
    Task<ArticoloDto> OttieniArticoloByIdAsync(int id);
    Task SalvaModificheArticolo(ArticoloDto articoloModificato);
}