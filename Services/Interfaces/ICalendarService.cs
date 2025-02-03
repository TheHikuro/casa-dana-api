using CasaDanaAPI.DTOs.Calendars;

namespace CasaDanaAPI.Services.Interfaces
{
    public interface ICalendarService
    {
        Task<int> GetPriceForDateAsync(DateTime date);
        
        Task<IEnumerable<CalendarDto>> GetAllCalendarEntriesAsync();
        Task<CalendarDto> GetCalendarEntryByIdAsync(Guid id);
        Task<CalendarDto> CreateCalendarEntryAsync(CalendarDto entryDto);
        Task UpdateCalendarEntryAsync(Guid id, CalendarDto entryDto);
        Task DeleteCalendarEntryAsync(Guid id);
    }
}