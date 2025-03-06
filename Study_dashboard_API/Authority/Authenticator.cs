using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Study_dashboard_API.Authority
{
    public static class Authenticator
    {
        public static bool Authenticate(string clientId, string secret)
        {
            var app = AppRepository.GetApplication(clientId);
            if (app == null)
            {
                return false;
            }
            return (app.ClientId == clientId && app.Secret == secret);
        }

        // Tworzymy token JSON JWT (Json web token)
        // Składa sie on z 3 części:
        // algorytm
        // Payload (ladunek) (claims)
        // Signature key (podpis)

        //Nuget packet: System.IdentityModel.Tokens.Jwt
        public static string CreateToken(string clientId, DateTime expireAt, string strSecretKey)
        {
            var app = AppRepository.GetApplication(clientId);

            var claims = new List<Claim>
            {
                new Claim("AppName", app.ApplicationName??string.Empty), //jeśli null = empty
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

        public static bool VerifyToken(string token, string strSecretKey)
        {
            if (string.IsNullOrEmpty(token)) return false;

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
                return false;
            }
            catch
            {
                throw;
            }

            return securityToken != null;
        }
    }
}
