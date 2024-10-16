using Demo.Model;

namespace Demo.DataServices.Interface;

public interface ILoginLogDataServices
{
    Task AggiungiNuovoLoginLogAsync(LoginLog loginLog);
    Task AggiungiNuovoLoginLogAsync(string username, string ipAddress, DateTime dataLogin, bool successo, string motivoFallimento = "", string userAgent = "");
}