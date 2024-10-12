using Demo.ModelDto.Articolo;

namespace Demo.DataServices.Interface;

public interface IArticoloDataServices
{
    Task AggiungiNuovoArticolo(ArticoloDto articoloDto);
    Task<List<ArticoloDto>> OttieniElencoArticoliDtoAsync();
}