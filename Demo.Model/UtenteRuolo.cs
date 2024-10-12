namespace Demo.Model;

public class UtenteRuolo
{
    public int Id { get; set; }

    public int IdUtente { get; set; }
    public virtual Utente Utente { get; set; }

    public int IdRuolo { get; set; }
    public virtual Ruolo Ruolo { get; set; }
}