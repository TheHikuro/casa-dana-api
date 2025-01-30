using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;
using System.Text;
using CasaDanaAPI.Models.Users;

namespace CasaDanaAPI.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService()
        {
            Env.Load(); 

            _secretKey = Env.GetString("JWT_SECRET_KEY") ?? throw new Exception("JWT_SECRET_KEY is missing.");
            _issuer = Env.GetString("JWT_ISSUER") ?? throw new Exception("JWT_ISSUER is missing.");
            _audience = Env.GetString("JWT_AUDIENCE") ?? throw new Exception("JWT_AUDIENCE is missing.");
        }

        public string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_secretKey); 

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}