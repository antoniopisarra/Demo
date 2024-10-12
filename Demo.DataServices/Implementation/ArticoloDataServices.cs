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
        NomeArticolo = articolo.NomeArticolo
    };

    #endregion

    public async Task AggiungiNuovoArticolo(ArticoloDto articoloDto)
    {
        await dbContext.Articoli.AddAsync(new Articolo
        {
            NomeArticolo = articoloDto.NomeArticolo
        });
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<ArticoloDto>> OttieniElencoArticoliDtoAsync()
    {
        return await dbContext.Articoli.AsNoTracking().Select(ToArticoloDto).ToListAsync();
    }
}