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
        var articoloCreato = await articoloDataServices.AggiungiNuovoArticolo(articolo.ToArticolo());
        return Ok(articoloCreato.ToArticoloDto());
    }

    [HttpGet]
    public async Task<IActionResult> OttieniElencoArticoli()
    {
        return Ok(await articoloDataServices.OttieniElencoArticoliDtoAsync());
    }

    [HttpDelete]
    public async Task<IActionResult> EliminaArticolo(int id)
    {
        await articoloDataServices.EliminaArticoloByIdAsync(id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> OttieniArticoloById(int id)
    {
        return Ok(await articoloDataServices.OttieniArticoloByIdAsync(id));
    }

    [HttpPut]
    public async Task<IActionResult> SalvaModificheArticolo([FromBody] ArticoloDto articoloDto)
    {
        await articoloDataServices.SalvaModificheArticolo(articoloDto);
        return Ok();
    }
}