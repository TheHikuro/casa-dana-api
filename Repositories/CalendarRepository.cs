using CasaDanaAPI.Infrastructure;
using CasaDanaAPI.Models.Calendar;
using CasaDanaAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CasaDanaAPI.Repositories
{
    public class CalendarRepository(DataContext context) : ICalendarRepository
    {
        public async Task<Calendar?> GetByIdAsync(Guid id) => await context.Calendars.FindAsync(id);

        public async Task<IEnumerable<Calendar>> GetAllAsync() => await context.Calendars.ToListAsync();
        
        public async Task<Calendar> CreateAsync(Calendar entry)
        {
            context.Calendars.Add(entry);
            await context.SaveChangesAsync();
            return entry;
        }

        public async Task UpdateAsync(Calendar entry)
        {
            context.Calendars.Update(entry);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await context.Calendars.FindAsync(id);
            if (existing != null)
            {
                context.Calendars.Remove(existing);
                await context.SaveChangesAsync();
            }
        }
        
        public async Task<List<Calendar>> GetOverlappingRangesAsync(DateTime start, DateTime end)
        {
            return await context.Calendars
                .Where(c => c.StartDate <= end && c.EndDate >= start) 
                .OrderBy(c => c.StartDate) 
                .ToListAsync();
        }
    }
}