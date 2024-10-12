using Demo.DataServices.Interface;
using Demo.Logic.Mapper;
using Demo.ModelDto.Articolo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers;

[Route("api/[controller]/[action]")]
[Authorize]
public class ArticoloController(IArticoloDataServices articoloDataServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreaNuovoArticolo([FromBody] NuovoArticoloDto articolo)
    {
        await articoloDataServices.AggiungiNuovoArticolo(articolo.ToArticolo());
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> OttieniElencoArticoli()
    {
        return Ok(await articoloDataServices.OttieniElencoArticoliDtoAsync());
    }
}