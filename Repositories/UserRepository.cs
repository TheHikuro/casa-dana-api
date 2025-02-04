using Microsoft.EntityFrameworkCore;
using CasaDanaAPI.Data;
using CasaDanaAPI.Models.Users;
using CasaDanaAPI.Repositories.Interfaces;

namespace CasaDanaAPI.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(Guid id) => await context.Users.FindAsync(id);
        
        public async Task<List<User>> GetAllAsync() => await context.Users.ToListAsync();
        
        public async Task<User> AddAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
        
        public async Task<User?> GetUserByEmailAsync(string email) => await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}