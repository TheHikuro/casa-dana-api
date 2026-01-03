using System.Globalization;
using CasaDanaAPI.Repositories.Interfaces;
using CasaDanaAPI.Services.Interfaces;
using Calendar = CasaDanaAPI.Models.Calendar.Calendar;

namespace CasaDanaAPI.Services;

public class CalendarService(ICalendarRepository calendarRepository) : ICalendarService
{
    private const int DefaultPrice = 95;
    
    private static DateTime ConvertToDdMmYyyy(string dateStr)
    {
        if (!DateTime.TryParseExact(dateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            throw new ArgumentException("Invalid date format. Use DD/MM/YYYY.");

        return DateTime.SpecifyKind(date, DateTimeKind.Utc);
    }

    public async Task<List<(DateTime Date, int Price)>> GetPriceForDateRangeAsync(string start, string end)
    {
        var parsedStart = ConvertToDdMmYyyy(start);
        var parsedEnd = ConvertToDdMmYyyy(end);

        if (parsedStart >= parsedEnd) throw new ArgumentException("Start date must be before end date.");

        var overlappingEntries = await calendarRepository.GetOverlappingRangesAsync(parsedStart, parsedEnd);
        var priceList = new List<(DateTime Date, int Price)>();

        for (var date = parsedStart; date < parsedEnd; date = date.AddDays(1))
        {
            var price = overlappingEntries
                .FirstOrDefault(entry => entry.StartDate <= date && entry.EndDate >= date)?.Price ?? DefaultPrice;

            priceList.Add((date, price));
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
