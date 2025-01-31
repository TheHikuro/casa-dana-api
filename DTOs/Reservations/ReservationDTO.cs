using CasaDanaAPI.Enums;

namespace CasaDanaAPI.DTOs.Reservations;

public class ReservationDto
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int NumberOfPersons { get; set; }
    public decimal Price { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Description { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
}