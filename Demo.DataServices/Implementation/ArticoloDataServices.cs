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

    public async Task<Articolo> AggiungiNuovoArticolo(Articolo articolo)
    {
        await dbContext.Articoli.AddAsync(articolo);
        await dbContext.SaveChangesAsync();
        return articolo;
    }

    public async Task<List<ArticoloDto>> OttieniElencoArticoliDtoAsync() =>
        await dbContext.Articoli.AsNoTracking().Select(ToArticoloDto).ToListAsync();

    public async Task EliminaArticoloByIdAsync(int id)
    {
        var articolo = await dbContext.Articoli.FindAsync(id);

        if (articolo is not null)
        {
            dbContext.Articoli.Remove(articolo);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<ArticoloDto> OttieniArticoloByIdAsync(int id) =>
        await dbContext.Articoli.AsNoTracking().Where(a => a.Id == id).Select(ToArticoloDto).FirstAsync();


    public async Task SalvaModificheArticolo(ArticoloDto articoloModificato)
    {
        var originale = await dbContext.Articoli.FindAsync(articoloModificato.Id);

        if (originale is not null)
        {
            originale.NomeArticolo = articoloModificato.NomeArticolo;
        }

        await dbContext.SaveChangesAsync();
    }
}