using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDanaAPI.Extensions
{
    public static class CorsExtensions
    {
        private const string CorsPolicyName = "CasaDanaCorsPolicy"; 

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, policy =>
                {
                    policy.WithOrigins("https://www.casa-dana.com") 
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); 
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            return app.UseCors(CorsPolicyName);
        }
    }
}