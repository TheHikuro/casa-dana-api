using CasaDanaAPI.Data;
using CasaDanaAPI.Models;
using CasaDanaAPI.Models.Calendar;
using CasaDanaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CasaDanaAPI.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly ApplicationDbContext _context;

        public CalendarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Calendar> CreateAsync(Calendar entry)
        {
            _context.Calendars.Add(entry);
            await _context.SaveChangesAsync();
            return entry;
        }

        public async Task<Calendar> GetByIdAsync(Guid id)
        {
            return await _context.Calendars.FindAsync(id);
        }

        public async Task<IEnumerable<Calendar>> GetAllAsync()
        {
            return await _context.Calendars.ToListAsync();
        }

        public async Task UpdateAsync(Calendar entry)
        {
            _context.Calendars.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _context.Calendars.FindAsync(id);
            if (existing != null)
            {
                _context.Calendars.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Calendar>> GetOverlappingRangesAsync(DateTime date)
        {
            return await _context.Calendars
                .Where(c => c.StartDate <= date && c.EndDate >= date)
                .ToListAsync();
        }
    }
}