using CasaDanaAPI.Services;
using CasaDanaAPI.Services.Interfaces;
using CasaDanaAPI.Repositories;
using CasaDanaAPI.Repositories.Interfaces;

namespace CasaDanaAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            
            services.AddScoped<TokenService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ICalendarService, CalendarService>();

            return services;
        }
    }
}