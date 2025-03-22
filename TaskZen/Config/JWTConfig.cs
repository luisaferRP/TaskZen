using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskZen.Config
{
    public class JWTConfig
    {
        public string Secret { get; set; }
        public int ExpirationIn { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        internal string GenerateJwtToken(int userId, string userName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Identificador único del token
                new Claim(JwtRegisteredClaimNames.UniqueName, userName), // Nombre de usuario
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()) // ID del usuario
            };


            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(ExpirationIn),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
