using System.ComponentModel.DataAnnotations;

namespace Demo.ModelDto.Utente;

public class LoginUtenteDto
{
    [Required(ErrorMessage = "Username obbligatoria")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password obbligatoria")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}