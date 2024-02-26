using System.Security.Cryptography;
using System.Text;

namespace ArtBiathlon.Services.Helpers;

public static class HashPassword
{
    public static byte[] GetPasswordHash(string password)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(password));
    }

    private static bool IsPasswordEqualsToHash(string password, byte[] passwordHash)
    {
        return GetPasswordHash(password).SequenceEqual(passwordHash);
    }
    
    public static void ThrowIfPasswordNotEqualsToHash(
        string password, 
        byte[] passwordHash)
    {
        if (!IsPasswordEqualsToHash(password, passwordHash))
        {
            throw new Exception(); // написать свои исключения
        }
    }
}