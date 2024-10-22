using Demo.HttpRest.Implementation;
using Demo.HttpRest.Interface;

namespace Demo.Client.Configuration;

public static class ServicesContainer
{
    public static void DataServices(this IServiceCollection service)
    {
        service.AddScoped<IUtenteRestServices, UtenteRestServices>();
        service.AddScoped<IArticoloRestServices, ArticoloRestServices>();
        service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}