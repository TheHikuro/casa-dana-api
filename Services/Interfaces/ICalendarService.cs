using CasaDanaAPI.Models.Calendar;

namespace CasaDanaAPI.Services.Interfaces;

public interface ICalendarService
{
    Task<int> GetPriceForDateAsync(DateTime date);
    Task<IEnumerable<Calendar>> GetAllCalendarEntriesAsync();
    Task<Calendar?> GetCalendarEntryByIdAsync(Guid id);
    Task<Calendar> CreateCalendarEntryAsync(Calendar entry);
    Task<Calendar?> UpdateCalendarEntryAsync(Guid id, Calendar entry);
    Task DeleteCalendarEntryAsync(Guid id);
}