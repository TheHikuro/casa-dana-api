using CasaDanaAPI.DTOs.Users;

namespace CasaDanaAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserByIdAsync(Guid id);
        Task<List<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> CreateUserAsync(CreateUserDto userDto);
        Task<string?> LoginAsync(LoginUserDto loginDto);
    }
}