using Demo.AuditService;

namespace Demo.Api.Configuration;

public class AuditServices(IHttpContextAccessor contextAccessor) : IAuditServices
{
    public string OttieniUtenteCollegato()
    {
        return contextAccessor.HttpContext?.User?.Identity?.Name ?? "Anonimo";
    }
}