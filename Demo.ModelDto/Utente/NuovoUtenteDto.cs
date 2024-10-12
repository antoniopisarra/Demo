namespace Demo.ModelDto.Utente;

public class NuovoUtenteDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<string> Ruoli { get; set; }
}