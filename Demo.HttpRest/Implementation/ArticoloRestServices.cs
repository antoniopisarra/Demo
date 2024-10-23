using Demo.HttpRest.Interface;
using Demo.ModelDto.Articolo;
using RestSharp;
using static Demo.HttpRest.DefaultClient;

namespace Demo.HttpRest.Implementation;

public class ArticoloRestServices : IArticoloRestServices
{
    private const string controller = "/Articolo";

    public async Task<List<ArticoloDto>?> OttieniElencoArticoliAsync(string tokenJwt) =>
     await OttieniClient().GetAsync<List<ArticoloDto>>(NuovaRequest($"{controller}/OttieniElencoArticoli", jwtToken: tokenJwt));

    public async Task<ArticoloDto?> CreaNuovoArticolo(NuovoArticoloDto nuovoArticoloDto, string tokenJwt) =>
        await OttieniClient().PostAsync<ArticoloDto>(NuovaRequest($"{controller}/CreaNuovoArticolo", nuovoArticoloDto, tokenJwt));

    public async Task CancellaArticolo(int id, string tokenJwt) =>
        await OttieniClient().DeleteAsync(NuovaRequest($"{controller}/EliminaArticolo", jwtToken: tokenJwt).AddParameter("id", id));

    public async Task<ArticoloDto?> OttieniArticoloById(int id, string tokenJwt) =>
        await OttieniClient().GetAsync<ArticoloDto>(NuovaRequest($"{controller}/OttieniArticoloById", jwtToken: tokenJwt).AddParameter("id", id));

    public async Task ModificaArticolo(ArticoloDto articoloDaAggiornare, string tokenJwt) =>
        await OttieniClient().PutAsync(NuovaRequest($"{controller}/SalvaModificheArticolo", articoloDaAggiornare, tokenJwt));

}