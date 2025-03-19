using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Study_dashboard_API.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Study_dashboard_API.Authority
{
    // Handles authentication logic and JWT token creation/validation
    public static class Authenticator
    {
        // Verifies if the application credentials (clientId and secret) are valid
        public static bool Authenticate(string clientId, string secret)
        {
            var app = AppRepository.GetApplication(clientId);
            if (app == null)
            {
                return false;
            }
            return (app.ClientId == clientId && app.Secret == secret);
        }

        // Creates a JWT token for the authenticated user with claims
        public static string CreateToken(string clientId, string userId, DateTime expireAt, string strSecretKey)
        {
            var app = AppRepository.GetApplication(clientId);

            var claims = new List<Claim>
            {
                new Claim("AppName", app.ApplicationName??string.Empty),
                new Claim("UserId", userId),
                new Claim("Read", (app?.Scopes??string.Empty).Contains("read")?"true": "false"),
                new Claim("Write", (app?.Scopes??string.Empty).Contains("write")?"true": "false"),
            };

            var secretKey = Encoding.ASCII.GetBytes(strSecretKey);

            var jwt = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
                claims: claims,
                expires: expireAt,
                notBefore: DateTime.UtcNow
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        // Validates and decodes a JWT token using the provided secret key
        public static JwtSecurityToken? VerifyToken(string token, string strSecretKey)
        {
            if (string.IsNullOrEmpty(token)) return null;

            if (token.StartsWith("Bearer")) token = token.Substring(6).Trim();

            var secretKey = Encoding.ASCII.GetBytes(strSecretKey);
            SecurityToken securityToken;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero,

                },
                out securityToken);
            }
            catch (SecurityTokenException)
            {
                return null;
            }
            catch
            {
                throw;
            }

            return securityToken as JwtSecurityToken;
        }
    }
}
