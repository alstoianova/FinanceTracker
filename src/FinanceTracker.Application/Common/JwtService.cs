using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceTracker.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace FinanceTracker.Application.Common;

public static class JwtService
{
    private const string SecretKey = "FinanceTrackerSuperSecretKey2026ForJwtAuthentication123456";
    private static readonly byte[] KeyBytes = Encoding.UTF8.GetBytes(SecretKey);

    public static string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(KeyBytes),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}