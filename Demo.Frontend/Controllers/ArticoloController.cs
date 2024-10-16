using Demo.ModelDto.Articolo;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Frontend.Controllers;

public class ArticoloController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(new List<ArticoloDto>{new()
        {
            Id = 1,
            NomeArticolo = "Prova"
        }});
    }
}