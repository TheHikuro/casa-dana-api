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
        public int NumberOfPersons { get; set; } = 1; 
        
        [Required]
        public DateTime Start { get; set; } = DateTime.UtcNow; 
        
        [Required]
        public DateTime End { get; set; } = DateTime.UtcNow.AddDays(7); 
        
        [Required]
        [MaxLength(50)]
        public decimal Price { get; set; } = 0; 
        
        [Required]
        [MaxLength(50)]
        public string Phone { get; set; } = string.Empty; 
        
        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty; 
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty; 
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty; 

        [MaxLength(255)] 
        public string? Description { get; set; } = string.Empty; 
        
        [Column(TypeName = "varchar(20)")]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending; 
    }
}