using Demo.Client.Configuration;
using Demo.HttpRest.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Client.Controllers;

[Authorize]
public class ArticoloController(IArticoloRestServices articoloRestServices, JwtService jwtService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var elencoArticoli =
            await articoloRestServices.OttieniElencoArticoliAsync(jwtService.OttieniJwtTokenDaCookie());
        return elencoArticoli is not null ? View(elencoArticoli) : View();
    }
}