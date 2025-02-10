using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CasaDanaAPI.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }
    }
}