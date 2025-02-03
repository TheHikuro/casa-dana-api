using AutoMapper;
using CasaDanaAPI.Mappings;

namespace CasaDanaAPI.Extensions
{
    public static class MappingExtensions
    {
        public static TTarget MapTo<TTarget>(this object source, IMapper mapper)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return mapper.Map<TTarget>(source);
        }
        
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CalendarProfile), typeof(ReservationProfile));

            return services;
        }
    }
}