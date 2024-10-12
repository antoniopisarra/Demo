using Demo.DataServices.Interface;
using Demo.Logic.Mapper;
using Demo.ModelDto.Ruolo;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers;

[Route("api/[controller]/[action]")]
public class RuoloController(IRuoloDataServices ruoloDataServices) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> OttieniElencoRuoli()
    {
        return Ok(await ruoloDataServices.OttieniElencoRuoliDtoAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreaNuovoRuolo([FromBody] NuovoRuoloDto nuovoRuolo)
    {
        await ruoloDataServices.AggiungiNuovoRuoloAsync(nuovoRuolo.ToRuolo());
        return Ok();
    }
}