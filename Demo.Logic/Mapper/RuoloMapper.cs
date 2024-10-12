using Demo.Model;
using Demo.ModelDto.Ruolo;

namespace Demo.Logic.Mapper;

public static class RuoloMapper
{
    public static Ruolo ToRuolo(this NuovoRuoloDto ruolo)
    {
        return new Ruolo
        {
            TipoRuolo = ruolo.TipoRuolo,
            Descrizione = ruolo.Descrizione
        };
    }

    public static RuoloDto ToRuoloDto(this Ruolo ruolo)
    {
        return new RuoloDto
        {
            Id = ruolo.Id,
            TipoRuolo = ruolo.TipoRuolo,
            Descrizione = ruolo.Descrizione
        };
    }
}