using AutoMapper;
using CasaDanaAPI.DTOs.Calendars;
using CasaDanaAPI.Models.Calendar;

namespace CasaDanaAPI.Mappings
{
    public class CalendarProfile : Profile
    {
        public CalendarProfile()
        {
            CreateMap<Calendar, CalendarDto>().ReverseMap();
        }
    }
}