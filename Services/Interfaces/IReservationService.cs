using CasaDanaAPI.DTOs.Reservations;

namespace CasaDanaAPI.Services.Interfaces;

public interface IReservationService
{
    public Task<ReservationDto> GetReservationByIdAsync(Guid id);
    public Task<List<ReservationDto>> GetAllReservationsAsync();
    public Task<ReservationDto> CreateReservationAsync(ReservationDto reservationDto);
    public Task<ReservationDto> UpdateReservationAsync(ReservationDto reservationDto);
    public Task<ReservationDto?> DeleteReservationAsync(Guid id);
}