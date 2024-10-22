using Demo.ModelDto.Articolo;

namespace Demo.HttpRest.Interface;

public interface IArticoloRestServices
{
    Task<List<ArticoloDto>?> OttieniElencoArticoliAsync(string tokenJwt);
}