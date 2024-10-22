using Demo.Client.Configuration;
using Demo.HttpRest.Interface;
using Demo.ModelDto.Utente;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Client.Controllers;

public class AccountController(IUtenteRestServices utenteRestServices, JwtService jwtService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUtenteDto loginUtente)
    {
        if (ModelState.IsValid)
        {
            var esitoLogin = await utenteRestServices.EffettuaLoginUtente(loginUtente);
            if (esitoLogin != null)
            {
                if (esitoLogin.Successo)
                {
                    //Response.Cookies.Append("jwtToken", esitoLogin.Token, new CookieOptions
                    //{
                    //    HttpOnly = true,
                    //    Secure = false,
                    //    Expires = DateTimeOffset.UtcNow.AddHours(1),
                    //    Path = "/"
                    //});

                    jwtService.AggiungiCookieJwtToken(esitoLogin.Token);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, $"{esitoLogin.MessaggioErrore}");
            }
        }

        return View("Index", loginUtente);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwtToken");
        return View("LogoutSuccess");
    }

    [HttpGet]
    public IActionResult SezioneRiservata()
    {
        return View();
    }
}