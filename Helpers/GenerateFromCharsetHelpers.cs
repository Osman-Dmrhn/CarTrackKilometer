using System.Security.Cryptography;

internal static class GenerateFromCharsetHelpers
{
    private static string GenerateFromCharset(string charset, int length)
    {
        var result = new char[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            var data = new byte[length];
            rng.GetBytes(data);

            for (int i = 0; i < length; i++)
            {
                var index = data[i] % charset.Length;
                result[i] = charset[index];
            }
        }
        return new string(result);
    }

    public static string GenerateStrongPassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{};:,.<>?";
        return GenerateFromCharset(chars, length);
    }

    public static string GenerateAlphaNumericPassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return GenerateFromCharset(chars, length);
    }

    public static string GenerateNumericPassword(int length)
    {
        const string digits = "0123456789";
        return GenerateFromCharset(digits, length);
    }
}