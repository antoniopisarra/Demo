using Demo.Api.Configuration;
using Demo.DataServices.Interface;
using Demo.Logic;
using Demo.Logic.Mapper;
using Demo.ModelDto.Utente;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers;

[Route("api/[controller]/[action]")]
public class UtenteController(IUtenteDataServices utenteDataServices, IRuoloDataServices ruoloDataServices, JwtService jwtService) : ControllerBase
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
        await utenteDataServices.AggiungiNuovoUtenteAsync(nuovoUtenteDto.ToUtente(await ruoloDataServices.OttieniElencoRuoliDtoAsync()));
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUtenteDto utenteDto)
    {
        var utente = await utenteDataServices.OttieniUtenteByUsernameAsync(utenteDto.Username);
        var controlloPassword = false;

        if (utente != null)
        {
            controlloPassword = PasswordHasher.VerificaPassword(utenteDto.Password, utente.PasswordHash);
        }

        if (utente == null || !controlloPassword)
        {
            return Unauthorized(new EsitoLoginDto
            {
                Username = utenteDto.Username,
                Successo = false,
                MessaggioErrore = "Credenziali non valide. Assicurati che utente e password siano corretti."
            });
        }

        return Ok(new EsitoLoginDto
        {
            Username = utenteDto.Username,
            Successo = true,
            Token = jwtService.GeneraToken(utenteDto)
        });
    }
}