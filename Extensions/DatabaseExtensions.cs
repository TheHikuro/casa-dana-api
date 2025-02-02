using Microsoft.EntityFrameworkCore;
using CasaDanaAPI.Data;
using DotNetEnv;

namespace CasaDanaAPI.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            Env.Load(); 
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            var connectionString = !string.IsNullOrEmpty(databaseUrl) && databaseUrl.StartsWith("postgres://")
                ? BuildConnectionStringFromUrl(databaseUrl)
                : BuildConnectionStringFromEnv();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }

        private static string BuildConnectionStringFromUrl(string databaseUrl)
        {
            var uri = new Uri(databaseUrl);
            var userInfo = uri.UserInfo.Split(':');
            return $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true;";
        }

        private static string BuildConnectionStringFromEnv()
        {
            return $"Host={Env.GetString("DATABASE_HOST", Environment.GetEnvironmentVariable("DATABASE_HOST"))};" +
                   $"Port={Env.GetString("DATABASE_PORT", Environment.GetEnvironmentVariable("DATABASE_PORT"))};" +
                   $"Database={Env.GetString("DATABASE_NAME", Environment.GetEnvironmentVariable("DATABASE_NAME"))};" +
                   $"Username={Env.GetString("DATABASE_USER", Environment.GetEnvironmentVariable("DATABASE_USER"))};" +
                   $"Password={Env.GetString("DATABASE_PASSWORD", Environment.GetEnvironmentVariable("DATABASE_PASSWORD"))};";
        }
    }
}