using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;

namespace CasaDanaAPI.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            Env.Load();

            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "fallbackKey";
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "fallbackIssuer";
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "fallbackAudience";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
                    };
                });

            return services;
        }
    }
}