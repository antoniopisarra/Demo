using Demo.HttpRest.Interface;
using Demo.ModelDto.Utente;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Client.Controllers;

public class AccountController(IUtenteRestServices utenteRestServices) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUtenteDto loginUtente)
    {
        var esitoLogin = await utenteRestServices.EffettuaLoginUtente(loginUtente);
        if (esitoLogin != null)
        {
            if (esitoLogin.Successo)
            {
                Response.Cookies.Append("jwtToken", esitoLogin.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    Expires = DateTimeOffset.UtcNow.AddHours(1),
                    Path = "/"
                });

                return RedirectToAction("Index", "Home");
            }
        }

        return Ok();
    }

    [HttpGet]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwtToken");
        return View("LogoutSuccess");
    }
}