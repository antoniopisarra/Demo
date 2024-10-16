using Demo.DataAccess;
using Demo.DataServices.Interface;
using Demo.Model;

namespace Demo.DataServices.Implementation;

public class LoginLogDataServices(DemoDbContext dbContext) : ILoginLogDataServices
{
    public async Task AggiungiNuovoLoginLogAsync(LoginLog loginLog)
    {
        await dbContext.LoginLogs.AddAsync(loginLog);
        await dbContext.SaveChangesAsync();
    }

    public async Task AggiungiNuovoLoginLogAsync(string username, string ipAddress, DateTime dataLogin, bool successo, string motivoFallimento = "", string userAgent = "")
    {
        await dbContext.LoginLogs.AddAsync(new LoginLog
        {
            Username = username,
            IpAddress = ipAddress,
            DataLogin = dataLogin,
            Successo = successo,
            MotivoFallimento = motivoFallimento,
            UserAgent = userAgent
        });

        await dbContext.SaveChangesAsync();
    }
}