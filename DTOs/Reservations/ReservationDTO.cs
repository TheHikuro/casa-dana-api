using System.ComponentModel.DataAnnotations;
using CasaDanaAPI.Enums;

namespace CasaDanaAPI.DTOs.Reservations
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Une date de début est requise.")]
        public required DateTime Start { get; set; }
        
        [Required(ErrorMessage = "Une date de fin est requise.")]
        public required DateTime End { get; set; }
        
        [Required(ErrorMessage = "Un nombre de personnes est requis.")]
        public required int NumberOfPersons { get; set; }
        
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Un numéro de téléphone est requis.")]
        public required string Phone { get; set; }
        
        [Required(ErrorMessage = "Une adresse e-mail est requise.")]
        public required string Email { get; set; } 
        
        [Required(ErrorMessage = "Un prénom est requis.")]
        public required string FirstName { get; set; }
        
        [Required(ErrorMessage = "Un nom de famille est requis.")]
        public required string LastName { get; set; } 
        
        public string? Description { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    }
}