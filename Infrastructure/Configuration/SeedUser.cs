using Microsoft.AspNetCore.Identity;
using CasaDanaAPI.Infrastructure.Identity;

namespace CasaDanaAPI.Infrastructure.Configuration;

public class SeedUsers
{
    public static async Task CreateAdminUser(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminEmail = "loan.cleris@gmail.com";
            var adminPassword = configuration["Authentication:RootPassword"];

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Loan",
                    LastName = "CLERIS",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded) Console.WriteLine("✅ Admin user created successfully!");
                else
                {
                    foreach (var error in result.Errors) Console.WriteLine($"❌ Error: {error.Description}");
                }
            }
            else Console.WriteLine("⚠️ Admin user already exists.");
        }
    }
}