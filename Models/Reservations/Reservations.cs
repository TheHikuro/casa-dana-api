using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CasaDanaAPI.Enums;

namespace CasaDanaAPI.Models.Reservations
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [MaxLength(50)]
        public required int NumberOfPersons { get; set; }
        
        [Required]
        public required DateTime Start { get; set; }
        
        [Required]
        public required DateTime End { get; set; }
        
        [Required]
        [MaxLength(50)]
        public required decimal Price { get; set; }
        
        [Required]
        [MaxLength(50)]
        public required string Phone { get; set; } 
        
        [Required]
        [MaxLength(255)]
        public required string Email { get; set; } 
        
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; } 
        
        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [MaxLength(255)] public string? Description { get; set; } = String.Empty; 
        
        [Column(TypeName = "varchar(20)")]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending; 
    }
}