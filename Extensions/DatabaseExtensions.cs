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
            return $"Host={Env.GetString("PGHOST", Environment.GetEnvironmentVariable("PGHOST"))};" +
                   $"Port={Env.GetString("PGPORT", Environment.GetEnvironmentVariable("PGPORT"))};" +
                   $"Database={Env.GetString("PGDATABASE", Environment.GetEnvironmentVariable("PGDATABASE"))};" +
                   $"Username={Env.GetString("PGUSER", Environment.GetEnvironmentVariable("PGUSER"))};" +
                   $"Password={Env.GetString("PGPASSWORD", Environment.GetEnvironmentVariable("DATABASE_PASSWORD"))};";
        }
    }
}