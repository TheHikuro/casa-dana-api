using CasaDanaAPI.Models.Calendar;

namespace CasaDanaAPI.Repositories.Interfaces
{
    public interface ICalendarRepository
    {
        Task<Calendar> CreateAsync(Calendar entry);
        Task<Calendar> GetByIdAsync(Guid id);
        Task<IEnumerable<Calendar>> GetAllAsync();
        Task UpdateAsync(Calendar entry);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<Calendar>> GetOverlappingRangesAsync(DateTime date);
    }
}