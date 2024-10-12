using Demo.Model;
using Demo.ModelDto.Articolo;

namespace Demo.Logic.Mapper;

public static class ArticoloMapper
{
    public static ArticoloDto ToArticoloDto(this Articolo articolo)
    {
        return new ArticoloDto
        {
            Id = articolo.Id,
            NomeArticolo = articolo.NomeArticolo
        };
    }

    public static Articolo ToArticolo(this NuovoArticoloDto articolo)
    {
        return new Articolo
        {
            NomeArticolo = articolo.NomeArticolo
        };
    }
}