using System.Security.Cryptography;
using Application.Persistence.Services.HashServices;
using HashService.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace HashService.Service;

public class HashService : IHashService
{
    private readonly HashOptions _hashOptions;

    public HashService(HashOptions hashOptions)
    {
        _hashOptions = hashOptions;
    }

    public string HashPassword(string password)
    {
        byte[] salt = new byte[_hashOptions.SaltLength];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string hashPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: _hashOptions.Iterations,
            numBytesRequested: _hashOptions.KeyLength));

        return $"{Convert.ToBase64String(salt)}.{hashPassword}";
    }

    public bool VerifyPassword(string hashPassword, string rawPassword)
    {
        var parts = hashPassword.Split(".", 2);

        if (parts.Length != 2)
        {
            throw new FormatException("Unexpected hash format. Should be formatted as `{salt}.{hash}`");
        }

        var salt = Convert.FromBase64String(parts[0]);
        var hashPasswordWithoutSalt = parts[1];

        var inputHashPasswordWithoutSalt = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: rawPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: _hashOptions.Iterations,
            numBytesRequested: _hashOptions.KeyLength));

        return hashPasswordWithoutSalt == inputHashPasswordWithoutSalt;
    }
}