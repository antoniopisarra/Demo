using Demo.Client.Configuration;
using Demo.HttpRest.Interface;
using Demo.ModelDto.Articolo;
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

    public async Task<IActionResult> CreaArticolo(string nomeArticolo)
    {
        if (string.IsNullOrWhiteSpace(nomeArticolo)) return RedirectToAction("Index");

        await articoloRestServices.CreaNuovoArticolo(new NuovoArticoloDto { NomeArticolo = nomeArticolo },
              jwtService.OttieniJwtTokenDaCookie());

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> EliminaArticolo(int id)
    {
        await articoloRestServices.CancellaArticolo(id, jwtService.OttieniJwtTokenDaCookie());
        return RedirectToAction("Index");
    }

    //Richiama pagina per la modifica dell'articolo selezionato
    public async Task<IActionResult> ModificaArticolo(int id)
    {
        var articoloDaModificare =
            await articoloRestServices.OttieniArticoloById(id, jwtService.OttieniJwtTokenDaCookie());

        return articoloDaModificare is not null ? View(articoloDaModificare) : RedirectToAction("Index");
    }

    //Salva le modifiche all'articolo sul server
    [HttpPost]
    public async Task<IActionResult> SalvaModificheArticolo(ArticoloDto articoloModificato)
    {
        await articoloRestServices.ModificaArticolo(articoloModificato, jwtService.OttieniJwtTokenDaCookie());
        return RedirectToAction("Index");
    }
}