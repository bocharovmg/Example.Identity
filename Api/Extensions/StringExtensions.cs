using System.Security.Cryptography;
using System.Text;

namespace Api.Extensions;

public static class StringExtensions
{
    public static string? GetHash(this string? str, HashAlgorithm hashAlgorithm)
    {
        if (hashAlgorithm == null)
        {
            return null;
        }

        using var algorithm = hashAlgorithm;

        if (str == null)
        {
            return null;
        }

        var bytes = Encoding.UTF8.GetBytes(str);

        var hash = algorithm.ComputeHash(bytes);

        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}
