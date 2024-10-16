using Demo.AuditService;
using Demo.DataServices.Implementation;
using Demo.DataServices.Interface;

namespace Demo.Api.Configuration;

public static class ServicesContainer
{
    public static void DataServices(this IServiceCollection service)
    {
        service.AddScoped<IUtenteDataServices, UtenteDataServices>();
        service.AddScoped<IRuoloDataServices, RuoloDataServices>();
        service.AddScoped<IArticoloDataServices, ArticoloDataServices>();
        service.AddScoped<IAuditServices, AuditServices>();
        service.AddScoped<ILoginLogDataServices, LoginLogDataServices>();
    }
}