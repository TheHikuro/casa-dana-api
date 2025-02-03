using Microsoft.AspNetCore.Mvc;
using CasaDanaAPI.DTOs.Users;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CasaDanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Create(CreateUserDto userDto)
        {
            var user = await userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginDto)
        {
            var token = await userService.LoginAsync(loginDto);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }
        
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user is null) return NotFound();

            return Ok(user);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}