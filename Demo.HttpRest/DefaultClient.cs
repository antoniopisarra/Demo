using RestSharp;

namespace Demo.HttpRest;

public static class DefaultClient
{
#if DEBUG
    private const string _defaultBaseUrl = "https://localhost:7232/api/";

#else
    private const string _defaultBaseUrl = "";
#endif

    public static RestClient OttieniClient()
    {
        var opt = new RestClientOptions(_defaultBaseUrl);

        return new RestClient(opt);
    }

    public static RestRequest AutenticazioneJwt(this RestRequest request, string tokenJwt)
    {
        return request.AddHeader("Authorization", $"Bearer {tokenJwt}");
    }

    public static RestRequest NuovaRequest(string percorso, object body = null, string jwtToken = "")
    {
        var request = new RestRequest(percorso);

        if (body is not null)
        {
            request.AddJsonBody(body);
        }

        if (!string.IsNullOrWhiteSpace(jwtToken))
        {
            request.AutenticazioneJwt(jwtToken);
        }

        

        

        return request;
    }
}