namespace Demo.Model;

public class Utente
{
    public Utente()
    {
        Ruoli = new HashSet<UtenteRuolo>();
    }
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public virtual ICollection<UtenteRuolo> Ruoli { get; set; }
}