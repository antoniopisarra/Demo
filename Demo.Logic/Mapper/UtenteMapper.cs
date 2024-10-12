using Demo.Model;
using Demo.ModelDto.Utente;

namespace Demo.Logic.Mapper;

public static class UtenteMapper
{
    public static Utente ToUtente(this NuovoUtenteDto utente, List<Ruolo> elencoRuoli)
    {
        return new Utente()
        {
            PasswordHash = PasswordHasher.HashPassword(utente.Username),
            Username = utente.Username
        };
    }
}