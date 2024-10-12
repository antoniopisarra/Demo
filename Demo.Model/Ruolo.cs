

namespace Demo.Model;

public class Ruolo
{
    public Ruolo()
    {
        Utenti = new HashSet<UtenteRuolo>();
    }
    public int Id { get; set; }
    public string TipoRuolo { get; set; }
    public string Descrizione { get; set; }

    public virtual ICollection<UtenteRuolo> Utenti { get; set; }
}