using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Services.Interfaces;
using CasaDanaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CasaDanaAPI.Services;

public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext _context;

    public ReservationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetReservationByIdAsync(Guid id)
    {
        return await _context.Reservations.FindAsync(id);
    }

    public async Task<List<Reservation>> GetAllReservationsAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<Reservation> CreateReservationAsync(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
    {
        _context.Reservations.Update(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task<bool> DeleteReservationAsync(Guid id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null) return false;

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return true;
    }
}