using Demo.HttpRest.Interface;
using Demo.ModelDto.Utente;
using RestSharp;
using static Demo.HttpRest.DefaultClient;

namespace Demo.HttpRest.Implementation;

public class UtenteRestServices : IUtenteRestServices
{
    public async Task<EsitoLoginDto?> EffettuaLoginUtente(LoginUtenteDto loginUtenteDto)
    {
        var request = new RestRequest("Utente/Login")
        {
            Method = Method.Post
        }.AddJsonBody(loginUtenteDto);

        return await OttieniClient().PostAsync<EsitoLoginDto>(request);
    }
}