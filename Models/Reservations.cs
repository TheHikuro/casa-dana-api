using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasaDanaAPI.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int NumberOfPersons { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal Price { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        [Column(TypeName = "varchar(20)")]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending; 
    }
}