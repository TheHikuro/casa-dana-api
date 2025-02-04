using System.ComponentModel.DataAnnotations;
    
    namespace CasaDanaAPI.Models.Users
    {
        public class User
        {
            [Key]
            public Guid Id { get; } = Guid.NewGuid();
    
            [Required]
            [MaxLength(255)]
            public required string Email { get; init; }
    
            [Required]
            [MaxLength(100)]
            public required string FirstName { get; init; }
    
            [Required]
            [MaxLength(100)]
            public required string LastName { get; init; }
            
            [Required]
            [MaxLength(100)]
            public required string PasswordHash { get; init; }
        }
    }