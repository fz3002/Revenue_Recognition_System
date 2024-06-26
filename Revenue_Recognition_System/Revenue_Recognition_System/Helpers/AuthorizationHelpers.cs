using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace Revenue_Recognition_System.Helpers;

public static class AuthorizationHelpers
{
    public static Tuple<string, string> GetHashedPasswordAndSalt(string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 12000,
                numBytesRequested: 256 / 8));

            var saltBase64 = Convert.ToBase64String(salt);

            return new Tuple<string, string>(hashed, saltBase64);
        }

        public static string GetHashedPasswordWithSalt(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            var currentHashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return currentHashedPassword;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static string GetUserIdFromAccessToken(string accessToken, string secret)
        {
            var tokenValidationParamters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateActor = true,
                ClockSkew = TimeSpan.FromMinutes(2),
                ValidIssuer = "https://localhost:5001",
                ValidAudience = "https://localhost:5001",
                ValidateLifetime = false,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secret)
                    )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParamters, out securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token!");
            }

            var userId = principal.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new SecurityTokenException($"Missing claim: {ClaimTypes.Name}!");
            }

            return userId;
        }
}