using CasaDanaAPI.Infrastructure;
using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CasaDanaAPI.Repositories;

public class ReservationRepository(DataContext context) : IReservationRepository
{
    public async Task<Reservation?> GetByIdAsync(Guid id) => await context.Reservations.FindAsync(id);
    
    public async Task<List<Reservation>> GetAllAsync() => await context.Reservations.ToListAsync();
    
    public async Task<Reservation> AddAsync(Reservation reservation)
    {
        context.Reservations.Add(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }

    public async Task<Reservation?> UpdateAsync(Guid id, Reservation reservation)
    {
        var existingReservation = await context.Reservations.FindAsync(id);
        if (existingReservation == null) return null;
        
        context.Reservations.Update(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }

    public async Task<Reservation?> DeleteAsync(Guid id)
    {
        var reservation = await context.Reservations.FindAsync(id);
        if (reservation == null) return null;

        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }
}