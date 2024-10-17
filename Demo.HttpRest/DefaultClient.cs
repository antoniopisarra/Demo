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
}