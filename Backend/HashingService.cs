using System.Security.Cryptography;
using System.Text;

namespace Backend; 

public static class HashingService {
    public static byte[] GenerateSalt()
        => RandomNumberGenerator.GetBytes(64);

    public static byte[] Hash(string password, byte[] salt) {
        using var hasher = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt,
            1_000_000, HashAlgorithmName.SHA512);
        return hasher.GetBytes(128);
    }
}
