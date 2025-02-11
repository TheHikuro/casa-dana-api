using CasaDanaAPI.Enums;
using CasaDanaAPI.Models.Reservations;

namespace CasaDanaAPI.Services.Interfaces;

public interface IReservationService
{
    public Task<Reservation?> GetReservationByIdAsync(Guid id);
    public Task<List<Reservation>> GetAllReservationsAsync();
    public Task<Reservation> CreateReservationAsync(Reservation reservation);
    public Task<Reservation?> UpdateReservationAsync(Guid id, ReservationStatus status);
    public Task<bool> DeleteReservationAsync(Guid id);
}