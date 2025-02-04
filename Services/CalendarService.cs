using AutoMapper;
using CasaDanaAPI.Models.Calendar;
using CasaDanaAPI.Repositories.Interfaces;
using CasaDanaAPI.Services.Interfaces;

namespace CasaDanaAPI.Services;

public class CalendarService(ICalendarRepository calendarRepository) : ICalendarService
{
    private const int DefaultPrice = 88;

    public async Task<int> GetPriceForDateAsync(DateTime date) =>
        (await calendarRepository.GetOverlappingRangesAsync(date)).FirstOrDefault()?.Price ?? DefaultPrice;

    public async Task<IEnumerable<Calendar>> GetAllCalendarEntriesAsync() => await calendarRepository.GetAllAsync();

    public async Task<Calendar?> GetCalendarEntryByIdAsync(Guid id) => await calendarRepository.GetByIdAsync(id);

    public async Task<Calendar> CreateCalendarEntryAsync(Calendar entry) => await calendarRepository.CreateAsync(entry);

    public async Task<Calendar?> UpdateCalendarEntryAsync(Guid id, Calendar entry)
    {
        var existing = await calendarRepository.GetByIdAsync(id);
        if (existing is null) return null;

        existing.StartDate = entry.StartDate;
        existing.EndDate = entry.EndDate;
        existing.Price = entry.Price;
        
        await calendarRepository.UpdateAsync(existing);

        return existing;
    }

    public Task DeleteCalendarEntryAsync(Guid id) => calendarRepository.DeleteAsync(id);
}