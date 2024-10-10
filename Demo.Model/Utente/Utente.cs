﻿namespace Demo.Model.Utente;

public class Utente
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public virtual ICollection<UtenteRuolo> Ruoli { get; set; }
}