using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Services.Interfaces;
using CasaDanaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CasaDanaAPI.Services;

public class ReservationService(ApplicationDbContext context) : IReservationService
{
    public async Task<Reservation?> GetReservationByIdAsync(Guid id)
    {
        return await context.Reservations.FindAsync(id);
    }

    public async Task<List<Reservation>> GetAllReservationsAsync()
    {
        return await context.Reservations.ToListAsync();
    }

    public async Task<Reservation> CreateReservationAsync(Reservation reservation)
    {
        context.Reservations.Add(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }

    public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
    {
        context.Reservations.Update(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }

    public async Task<bool> DeleteReservationAsync(Guid id)
    {
        var reservation = await context.Reservations.FindAsync(id);
        if (reservation == null) return false;

        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync();
        return true;
    }
}