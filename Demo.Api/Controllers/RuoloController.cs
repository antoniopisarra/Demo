using Demo.DataServices.Interface;
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
}