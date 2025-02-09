using AutoMapper;
using CasaDanaAPI.Models.Calendar;
using CasaDanaAPI.Repositories.Interfaces;
using CasaDanaAPI.Services.Interfaces;

namespace CasaDanaAPI.Services;

public class CalendarService(ICalendarRepository calendarRepository) : ICalendarService
{
    private const int DefaultPrice = 88;
    
    private static DateTime NormalizeToUtc(DateTime date)
    {
        return date.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(date, DateTimeKind.Utc) : date.ToUniversalTime();
    }

    public async Task<List<(DateTime Date, int Price)>> GetPriceForDateRangeAsync(DateTime start, DateTime end)
    {
        if (start > end) throw new ArgumentException("Start date must be before end date.");

        start = NormalizeToUtc(start);
        end = NormalizeToUtc(end);

        var overlappingEntries = await calendarRepository.GetOverlappingRangesAsync(start, end);
        var priceList = new List<(DateTime Date, int Price)>();

        for (var date = start; date <= end; date = date.AddDays(1))
        {
            var price = overlappingEntries
                .FirstOrDefault(entry => entry.StartDate <= date && entry.EndDate >= date)?.Price ?? DefaultPrice;

            if (price != DefaultPrice) 
            {
                priceList.Add((date, price));
            }
        }

        return priceList;
    }


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