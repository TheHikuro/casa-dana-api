using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;
using CasaDanaAPI.Infrastructure.Identity; 
using Microsoft.AspNetCore.Identity;

namespace CasaDanaAPI.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(UserManager<ApplicationUser> userManager)
        {
            Env.Load();

            _secretKey = Env.GetString("JWT_SECRET_KEY") 
                         ?? throw new Exception("JWT_SECRET_KEY is missing.");
            _issuer = Env.GetString("JWT_ISSUER") 
                      ?? throw new Exception("JWT_ISSUER is missing.");
            _audience = Env.GetString("JWT_AUDIENCE") 
                        ?? throw new Exception("JWT_AUDIENCE is missing.");
            
            _userManager = userManager;
        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var keyBytes = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
