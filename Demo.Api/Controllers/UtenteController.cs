using Demo.Api.Configuration;
using Demo.DataServices.Interface;
using Demo.Logic;
using Demo.Logic.Mapper;
using Demo.ModelDto.Utente;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers;

[Route("api/[controller]/[action]")]
public class UtenteController(
    IUtenteDataServices utenteDataServices,
    IRuoloDataServices ruoloDataServices,
    JwtService jwtService,
    IHttpContextAccessor accessor,
    ILoginLogDataServices logDataServices) : ControllerBase
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
        var ipAddress = accessor.HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = accessor.HttpContext.Request.Headers.UserAgent.ToString();
        var dataLogin = DateTime.Now;

        var utente = await utenteDataServices.OttieniUtenteByUsernameAsync(utenteDto.Username);

        if (utente == null)
        {
            await logDataServices.AggiungiNuovoLoginLogAsync(utenteDto.Username, ipAddress, dataLogin, false,
                "Username Inesistente", userAgent);
            return Unauthorized(new EsitoLoginDto
            {
                Username = utenteDto.Username,
                Successo = false,
                MessaggioErrore = "Credenziali non valide. Assicurati che utente e password siano corretti."
            });
        }

        if (!PasswordHasher.VerificaPassword(utenteDto.Password, utente.PasswordHash))
        {
            await logDataServices.AggiungiNuovoLoginLogAsync(utenteDto.Username, ipAddress, dataLogin, false,
                "Password Errata", userAgent);

            return Unauthorized(new EsitoLoginDto
            {
                Username = utenteDto.Username,
                Successo = false,
                MessaggioErrore = "Credenziali non valide. Assicurati che utente e password siano corretti."
            });
        }

        await logDataServices.AggiungiNuovoLoginLogAsync(utenteDto.Username, ipAddress, dataLogin, true, userAgent: userAgent);

        return Ok(new EsitoLoginDto
        {
            Username = utenteDto.Username,
            Successo = true,
            Token = jwtService.GeneraToken(utenteDto)
        });
    }
}