using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Services.Interfaces;
using CasaDanaAPI.Repositories.Interfaces;

namespace CasaDanaAPI.Services;

public class ReservationService(IReservationRepository reservationRepository) : IReservationService
{
    public async Task<Reservation?> GetReservationByIdAsync(Guid id) => await reservationRepository.GetByIdAsync(id);

    public async Task<List<Reservation>> GetAllReservationsAsync() => await reservationRepository.GetAllAsync();
    
    public async Task<Reservation> CreateReservationAsync(Reservation reservationModel) => await reservationRepository.AddAsync(reservationModel);

    public async Task<Reservation?> UpdateReservationAsync(Guid id, Reservation reservation)
    {
        var existingReservation = await reservationRepository.GetByIdAsync(id);
        if (existingReservation is null) return null;
        
        return await reservationRepository.UpdateAsync(id, reservation);
    }

    public async Task<bool> DeleteReservationAsync(Guid id)
    {
        var existingReservation = await reservationRepository.GetByIdAsync(id);
        if (existingReservation is null) return false;

        await reservationRepository.DeleteAsync(id);
        
        return true;
    }
}