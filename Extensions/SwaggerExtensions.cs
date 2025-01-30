using Microsoft.OpenApi.Models;
using DotNetEnv;

namespace CasaDanaAPI.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerWithAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CasaDana API",
                    Version = "v1",
                    Description = "CasaDana Reservation API",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer YOUR_TOKEN' below.",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

                options.UseAllOfToExtendReferenceSchemas();
            });

            return services;
        }

        public static void UseSwaggerWithUi(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.{format}";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CasaDana API v1");
                c.RoutePrefix = "swagger";
            });

            Task.Run(async () =>
            {
                await Task.Delay(5000);
                await SaveOpenApiSpec();
            });
        }

        private static async Task SaveOpenApiSpec()
        {
            Env.Load();
            var specUrl = $"{Env.GetString("API_URL")}/swagger/v1/swagger.json";
            var specDirectory = "Specifications";
            var specFilePath = Path.Combine(specDirectory, "openapi.yaml");

            if (!Directory.Exists(specDirectory))
            {
                Directory.CreateDirectory(specDirectory);
            }

            var httpClient = new HttpClient();
            int maxRetries = 10;
            int delayMilliseconds = 2000;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    var response = await httpClient.GetStringAsync(specUrl);
                    await File.WriteAllTextAsync(specFilePath, response);
                    Console.WriteLine($"✅ OpenAPI spec saved to {specFilePath}");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Attempt {attempt}/{maxRetries} - OpenAPI spec not available yet. Retrying in {delayMilliseconds / 1000} seconds...");
                    await Task.Delay(delayMilliseconds);
                }
                Console.WriteLine($"❌ Failed to save OpenAPI spec after {maxRetries} attempts.");
            }

        }
    }
}