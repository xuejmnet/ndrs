using System.Security.Cryptography;
using System.Text;

namespace NDRS.MySqlServer.Utils;

public static class Sha1Helper
{
    public static string Hash(string input)
    {
        using var sha1 = SHA1.Create();
        return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(input)));
    }
    public static byte[] Hash2Bytes(string input)
    {
        using var sha1 = SHA1.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        return sha1.ComputeHash(bytes);
    }
    public static byte[] Hash2Bytes(byte[] inputBytes)
    {
        using var sha1 = SHA1.Create();
        return sha1.ComputeHash(inputBytes);
    }
    
}