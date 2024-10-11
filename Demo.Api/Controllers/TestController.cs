using Demo.DataServices.Interface;
using Demo.Logic;
using Demo.Model.Utente;
using Demo.ModelDto.Utente;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController(IUtenteDataServices utenteDataServices) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AggiungiNuovoUtente(NuovoUtenteDto utenteDto)
        {
            var utente = new Utente
            {
                Username = utenteDto.Username,
                PasswordHash = PasswordHasher.HashPassword(utenteDto.Password)
            };
            await utenteDataServices.AggiungiNuovoUtenteAsync(utente);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AggiornaUtente(Utente utente)
        {
            await utenteDataServices.AggiornaUtenteAsync(utente);
            return Ok();
        }
    }
}