using System.Security.Cryptography;

namespace ExamBuilder.Data;

/// <summary>
/// تجزئة كلمات المرور باستخدام PBKDF2 (native في .NET، بلا مكتبات خارجية).
/// الصيغة المخزّنة: base64(salt) : base64(hash)
/// </summary>
public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000;
    private static readonly HashAlgorithmName Algo = HashAlgorithmName.SHA256;

    public static string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algo, KeySize);
        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(key)}";
    }

    public static bool Verify(string password, string stored)
    {
        var parts = stored.Split(':', 2);
        if (parts.Length != 2) return false;
        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] expected = Convert.FromBase64String(parts[1]);
        byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algo, expected.Length);
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
