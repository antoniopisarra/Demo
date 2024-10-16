namespace Demo.Model;

public class LoginLog
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string IpAddress { get; set; }
    public DateTime DataLogin { get; set; }
    public bool Successo { get; set; }
    public string MotivoFallimento { get; set; }
    public string UserAgent { get; set; }
}