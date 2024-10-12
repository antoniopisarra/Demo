using System.Linq.Expressions;
using Demo.DataAccess;
using Demo.DataServices.Interface;
using Demo.Model;
using Demo.ModelDto.Articolo;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataServices.Implementation;

public class ArticoloDataServices(DemoDbContext dbContext) : IArticoloDataServices
{
    #region Proiezioni

    private static readonly Expression<Func<Articolo, ArticoloDto>> ToArticoloDto = articolo => new ArticoloDto
    {
        Id = articolo.Id,
        NomeArticolo = articolo.NomeArticolo
    };

    #endregion Proiezioni

    public async Task AggiungiNuovoArticolo(Articolo articolo)
    {
        await dbContext.Articoli.AddAsync(articolo);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<ArticoloDto>> OttieniElencoArticoliDtoAsync() =>
        await dbContext.Articoli.AsNoTracking().Select(ToArticoloDto).ToListAsync();
}