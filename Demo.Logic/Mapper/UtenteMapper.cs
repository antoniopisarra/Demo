using Demo.Model;
using Demo.ModelDto.Ruolo;
using Demo.ModelDto.Utente;

namespace Demo.Logic.Mapper;

public static class UtenteMapper
{
    public static Utente ToUtente(this NuovoUtenteDto utenteDto, List<RuoloDto> elencoRuoli)
    {
        var utente = new Utente
        {
            PasswordHash = PasswordHasher.HashPassword(utenteDto.Password),
            Username = utenteDto.Username
        };

        foreach (var ruolo in utenteDto.Ruoli.Select(tipoRuolo => elencoRuoli.First(dto => string.Equals(dto.TipoRuolo, tipoRuolo, StringComparison.CurrentCultureIgnoreCase))))
        {
            utente.Ruoli.Add(new UtenteRuolo
            {
                IdRuolo = ruolo.Id
            });
        }

        return utente;
    }
}