using AutoMapper;
using Demo.DataServices.Interface;
using Demo.Model.Utente;
using Demo.ModelDto.Utente;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers;

[Route("api/[controller]/[action]")]
public class UtenteDataServices(IMapper mapper, IUtenteDataServices utenteDataServices) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> VerificaEsistenzaNomeUtente(string username)
    {
        return await utenteDataServices.VerificaEsistenzaNomeUtenteAsync(username) ?
            Ok(new VerificaNomeUtenteDto
            {
                Disponibile = false,
                Messaggio = "Nome utente NON disponibile"
            }) :
            Ok(new VerificaNomeUtenteDto
            {
                Disponibile = true,
                Messaggio = "Nome utente disponibile"
            });
    }

    [HttpPost]
    public async Task<IActionResult> CreaNuovoUtente([FromBody] NuovoUtenteDto nuovoUtenteDto)
    {
        await utenteDataServices.AggiungiNuovoUtenteAsync(mapper.Map<Utente>(nuovoUtenteDto));
        return Ok();
    }






}