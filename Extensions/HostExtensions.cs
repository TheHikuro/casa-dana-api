using CasaDanaAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CasaDanaAPI.Extensions
{
    public static class HostExtensions
    {
        public static void MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            db.Database.Migrate();
        }
    }
}