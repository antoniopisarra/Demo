using Demo.ModelDto.Utente;

namespace Demo.HttpRest.Interface;

public interface IUtenteRestServices
{
    Task<EsitoLoginDto?> EffettuaLoginUtente(LoginUtenteDto loginUtenteDto);
}