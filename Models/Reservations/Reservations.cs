using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CasaDanaAPI.Enums;

namespace CasaDanaAPI.Models.Reservations
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();
        
        [Required]
        [MaxLength(50)]
        public int NumberOfPersons { get; init; } = 1; 
        
        [Required]
        public DateTime Start { get; init; } = DateTime.UtcNow; 
        
        [Required]
        public DateTime End { get; init; } = DateTime.UtcNow.AddDays(7); 
        
        [Required]
        [MaxLength(50)]
        public decimal Price { get; init; } = 0; 
        
        [Required]
        [MaxLength(50)]
        public string Phone { get; init; } = string.Empty; 
        
        [Required]
        [MaxLength(255)]
        public string Email { get; init; } = string.Empty; 
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; init; } = string.Empty; 
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; init; } = string.Empty; 

        [MaxLength(255)] 
        public string? Description { get; init; } = string.Empty; 
        
        [Column(TypeName = "varchar(20)")]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending; 
    }
}