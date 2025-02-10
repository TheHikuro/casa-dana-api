using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CasaDanaAPI.Infrastructure.Identity;
using CasaDanaAPI.Models.Users;
using CasaDanaAPI.Services;

namespace CasaDanaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            TokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return BadRequest(ModelState);
            }

            return Ok("User registered successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var signInResult = await _signInManager.PasswordSignInAsync(
                model.Email, 
                model.Password, 
                isPersistent: false, 
                lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                return Unauthorized("Invalid credentials");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("User not found");

            var token = await _tokenService.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                Email = model.Email
            });
        }
    }
}
