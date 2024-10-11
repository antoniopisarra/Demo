using AutoMapper;
using Demo.Logic;
using Demo.ModelDto.Utente;

namespace Demo.Model.AutoMapper;

public class UtenteProfile : Profile
{
    public UtenteProfile()
    {
        CreateMap<NuovoUtenteDto, Utente.Utente>()
            .ForMember(utente => utente.PasswordHash, opt => opt.MapFrom(dto => PasswordHasher.HashPassword(dto.Password)));
    }
}