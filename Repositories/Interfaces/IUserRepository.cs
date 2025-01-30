using CasaDanaAPI.Models.Users;

namespace CasaDanaAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task<User> AddAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}