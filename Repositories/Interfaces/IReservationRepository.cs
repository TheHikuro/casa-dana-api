using CasaDanaAPI.Models.Reservations;

namespace CasaDanaAPI.Repositories.Interfaces;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(Guid id);
    Task<List<Reservation>> GetAllAsync();
    Task<Reservation> AddAsync(Reservation reservation);
    Task<Reservation?> UpdateAsync(Guid id, Reservation reservation);
    Task<Reservation?> DeleteAsync(Guid id);
}