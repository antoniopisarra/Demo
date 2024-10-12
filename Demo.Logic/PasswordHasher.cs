namespace Demo.Logic;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }

    public static bool VerificaPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password.Trim(), passwordHash.Trim());
    }
}