using CasaDanaAPI.Enums;

namespace CasaDanaAPI.DTOs.Reservations
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int NumberOfPersons { get; set; }
        public decimal Price { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    }
}