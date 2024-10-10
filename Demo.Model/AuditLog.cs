namespace Demo.Model;

public class AuditLog
{
    public int Id { get; set; }
    public string TipoEvento { get; set; }
    public string NomeTabella { get; set; }
    public string ChiavePrimaria { get; set; }
    public string ValoriPrecedenti { get; set; }
    public string NuoviValori { get; set; }
    public string Utente { get; set; }
    public DateTime DataModifica { get; set; }
}