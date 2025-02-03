using AutoMapper;
using CasaDanaAPI.DTOs.Reservations;
using CasaDanaAPI.Models.Reservations;

namespace CasaDanaAPI.Mappings
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }
    }
}