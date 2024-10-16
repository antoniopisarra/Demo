using Demo.ModelDto.Utente;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Frontend.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Accesso()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Accesso(LoginUtenteDto loginUtenteDto)
    {
        return Ok();
    }
}