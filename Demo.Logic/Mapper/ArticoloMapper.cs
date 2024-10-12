using Demo.Model;
using Demo.ModelDto.Articolo;

namespace Demo.Logic.Mapper;

public static class ArticoloMapper
{
    public static ArticoloDto ToArticoloDto(this Articolo articolo)
    {
        return new ArticoloDto
        {
            NomeArticolo = articolo.NomeArticolo
        };
    }
}