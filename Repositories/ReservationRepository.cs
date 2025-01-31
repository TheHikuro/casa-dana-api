using CasaDanaAPI.Data;
    using CasaDanaAPI.Models.Reservations;
    using CasaDanaAPI.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;
    
    namespace CasaDanaAPI.Repositories;
    
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;
    
        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public async Task<Reservation?> GetByIdAsync(Guid id)
        {
            return await _context.Reservations.FindAsync(id);
        }
    
        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }
    
        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
    
        public async Task<Reservation?> UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
    
        public async Task<Reservation?> DeleteAsync(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return null;
    
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
    }