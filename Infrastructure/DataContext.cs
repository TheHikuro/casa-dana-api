using CasaDanaAPI.Infrastructure.Identity;
using CasaDanaAPI.Models.Calendar;
using CasaDanaAPI.Models.Reservations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasaDanaAPI.Infrastructure
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Calendar>()
                .Property(c => c.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Reservation>()
                .Property(r => r.Id)
                .HasDefaultValueSql("gen_random_uuid()");
        }
    }
}