using CasaDanaAPI.DTOs.Users;
            using CasaDanaAPI.Models.Users;
            using CasaDanaAPI.Repositories.Interfaces;
            using CasaDanaAPI.Services.Interfaces;
            
            namespace CasaDanaAPI.Services
            {
                public class UserService : IUserService
                {
                    private readonly IUserRepository _userRepository;
                    private readonly TokenService _tokenService;
            
                    public UserService(IUserRepository userRepository, TokenService tokenService)
                    {
                        _userRepository = userRepository;
                        _tokenService = tokenService;
                    }
            
                    private string HashPassword(string password)
                    {
                        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
                    }
            
                    private bool VerifyPassword(string password, string hashedPassword)
                    {
                        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                    }
            
                    public async Task<UserResponseDto> GetUserByIdAsync(Guid id)
                    {
                        var user = await _userRepository.GetByIdAsync(id);
                        if (user == null) return null;
            
                        return new UserResponseDto
                        {
                            Id = user.Id,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        };
                    }
            
                    public async Task<List<UserResponseDto>> GetAllUsersAsync()
                    {
                        var users = await _userRepository.GetAllAsync();
                        return users.Select(u => new UserResponseDto
                        {
                            Id = u.Id,
                            Email = u.Email,
                            FirstName = u.FirstName,
                            LastName = u.LastName
                        }).ToList();
                    }
            
                    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto userDto)
                    {
                        var hashedPassword = HashPassword(userDto.Password);
            
                        var user = new User
                        {
                            Email = userDto.Email,
                            FirstName = userDto.FirstName,
                            LastName = userDto.LastName,
                            PasswordHash = hashedPassword
                        };
            
                        var newUser = await _userRepository.AddAsync(user);
                        return new UserResponseDto
                        {
                            Id = newUser.Id,
                            Email = newUser.Email,
                            FirstName = newUser.FirstName,
                            LastName = newUser.LastName
                        };
                    }
            
                    public async Task<string?> LoginAsync(LoginUserDto loginDto)
                    {
                        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
                        if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash)) return null;

                        var token = _tokenService.GenerateToken(user);
                        return token;
                    }
                }
            }