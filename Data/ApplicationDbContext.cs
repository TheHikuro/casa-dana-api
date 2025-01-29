using Microsoft.EntityFrameworkCore;
using CasaDanaAPI.Models.Calendar;
using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Models.Users;

namespace CasaDanaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Calendar>().Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Reservation>().Property(r => r.Id).HasDefaultValueSql("gen_random_uuid()");
        }
    }
}