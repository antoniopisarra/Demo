using Demo.ModelDto.Utente;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Demo.Api.Configuration;

public class JwtService(IConfiguration configuration)
{
    public string GeneraToken(LoginUtenteDto utente)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
        var credenziali = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Name, utente.Username),
            new(ClaimTypes.Name, utente.Username),
        };

        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credenziali));
    }
}