namespace Demo.Model.Utente;

public class UtenteRuolo
{
    public int Id { get; set; }

    public int IdUtente { get; set; }
    public Utente Utente { get; set; }

    public int IdRuolo { get; set; }
    public Ruolo Ruolo { get; set; }
}