using System.ComponentModel.DataAnnotations;
    
    namespace CasaDanaAPI.Models.Users
    {
        public class User
        {
            [Key]
            public Guid Id { get; set; } = Guid.NewGuid();
    
            [Required]
            [MaxLength(255)]
            public required string Email { get; set; }
    
            [Required]
            [MaxLength(100)]
            public required string FirstName { get; set; }
    
            [Required]
            [MaxLength(100)]
            public required string LastName { get; set; }
            
            [Required]
            [MaxLength(100)]
            public required string PasswordHash { get; set; }
        }
    }