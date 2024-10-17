using System.ComponentModel.DataAnnotations;

namespace Demo.ModelDto.Utente;

public class LoginUtenteDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}