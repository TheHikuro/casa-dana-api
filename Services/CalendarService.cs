using CasaDanaAPI.DTOs.Calendars;
        using CasaDanaAPI.Models;
        using CasaDanaAPI.Models.Calendar;
        using CasaDanaAPI.Repositories.Interfaces;
        using CasaDanaAPI.Services.Interfaces;
        
        namespace CasaDanaAPI.Services
        {
            public class CalendarService : ICalendarService
            {
                private readonly ICalendarRepository _calendarRepo;
                private const int DefaultPrice = 88;
        
                public CalendarService(ICalendarRepository calendarRepo)
                {
                    _calendarRepo = calendarRepo;
                }
        
                public async Task<int> GetPriceForDateAsync(DateTime date)
                {
                    var overlapping = await _calendarRepo.GetOverlappingRangesAsync(date);
        
                    if (!overlapping.Any())
                        return DefaultPrice;
        
                    return overlapping.First().Price;
                }
        
                public async Task<IEnumerable<Calendar>> GetAllCalendarEntriesAsync()
                {
                    var entities = await _calendarRepo.GetAllAsync();
                    return entities.Select(e => new Calendar
                    {
                        Id = e.Id,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Price = e.Price
                    });
                }
        
                public async Task<Calendar> GetCalendarEntryByIdAsync(Guid id)
                {
                    var entity = await _calendarRepo.GetByIdAsync(id);
                    if (entity is null) return null;
        
                    return new Calendar
                    {
                        Id = entity.Id,
                        StartDate = entity.StartDate,
                        EndDate = entity.EndDate,
                        Price = entity.Price
                    };
                }
        
                public async Task<Calendar> CreateCalendarEntryAsync(Calendar entryDto)
                {
                    var entity = new Calendar
                    {
                        StartDate = entryDto.StartDate,
                        EndDate = entryDto.EndDate,
                        Price = entryDto.Price
                    };
        
                    var created = await _calendarRepo.CreateAsync(entity);
        
                    return new Calendar
                    {
                        Id = created.Id,
                        StartDate = created.StartDate,
                        EndDate = created.EndDate,
                        Price = created.Price
                    };
                }
        
                public async Task UpdateCalendarEntryAsync(Guid id, Calendar entryDto)
                {
                    var existing = await _calendarRepo.GetByIdAsync(id);
                    if (existing == null)
                    {
                        return;
                    }
        
                    existing.StartDate = entryDto.StartDate;
                    existing.EndDate = entryDto.EndDate;
                    existing.Price = entryDto.Price;
        
                    await _calendarRepo.UpdateAsync(existing);
                }
        
                public async Task DeleteCalendarEntryAsync(Guid id)
                {
                    await _calendarRepo.DeleteAsync(id);
                }
            }
        }