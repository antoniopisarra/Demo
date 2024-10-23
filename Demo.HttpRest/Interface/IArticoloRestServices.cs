using Demo.ModelDto.Articolo;

namespace Demo.HttpRest.Interface;

public interface IArticoloRestServices
{
    Task<List<ArticoloDto>?> OttieniElencoArticoliAsync(string tokenJwt);
    Task<ArticoloDto?> CreaNuovoArticolo(NuovoArticoloDto nuovoArticoloDto, string tokenJwt);
    Task CancellaArticolo(int id, string tokenJwt);
    Task ModificaArticolo(ArticoloDto articoloDaAggiornare, string tokenJwt);
    Task<ArticoloDto?> OttieniArticoloById(int id, string tokenJwt);
}