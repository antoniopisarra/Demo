using Demo.Model;
using Demo.ModelDto.Articolo;

namespace Demo.DataServices.Interface;

public interface IArticoloDataServices
{
    Task AggiungiNuovoArticolo(Articolo articolo);
    Task<List<ArticoloDto>> OttieniElencoArticoliDtoAsync();
}