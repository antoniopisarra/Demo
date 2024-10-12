namespace Demo.ModelDto.Utente;

public class EsitoLoginDto
{
    public bool Successo { get; set; }
    public string MessaggioErrore { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
}