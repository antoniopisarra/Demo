using Demo.HttpRest.Interface;
using Demo.ModelDto.Articolo;
using RestSharp;
using static Demo.HttpRest.DefaultClient;

namespace Demo.HttpRest.Implementation;

public class ArticoloRestServices : IArticoloRestServices
{
    public async Task<List<ArticoloDto>?> OttieniElencoArticoliAsync(string tokenJwt)
    {
        var request = new RestRequest("/Articolo/OttieniElencoArticoli").AutenticazioneJwt(tokenJwt);
        return await OttieniClient().GetAsync<List<ArticoloDto>>(request);
    }
}