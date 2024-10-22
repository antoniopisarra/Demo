namespace Demo.Client.Configuration;

public class JwtService(IHttpContextAccessor context)
{
    private const string nomeCookie = "jwtToken";

    public void AggiungiCookieJwtToken(string token)
    {
       context.HttpContext.Response.Cookies.Append(nomeCookie, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            Expires = DateTimeOffset.UtcNow.AddHours(1),
            Path = "/"
        });
    }

    public string OttieniJwtTokenDaCookie()
    {
        return context.HttpContext.Request.Cookies[nomeCookie];
    }

    public void RimuoviJwtToken()
    {
        context.HttpContext.Response.Cookies.Delete(nomeCookie);
    }
}