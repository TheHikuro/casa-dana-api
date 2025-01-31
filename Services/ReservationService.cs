using CasaDanaAPI.DTOs.Reservations;
using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Repositories.Interfaces;
using CasaDanaAPI.Services.Interfaces;

namespace CasaDanaAPI.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<ReservationDto> GetReservationByIdAsync(Guid id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        return reservation == null ? null : MapReservationToDto(reservation);
    }

    public async Task<List<ReservationDto>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllAsync();
        return reservations.Select(MapReservationToDto).ToList();
    }

    public async Task<ReservationDto> CreateReservationAsync(ReservationDto reservationDto)
    {
        var reservation = new Reservation()
        {
            Id = reservationDto.Id,
            NumberOfPersons = reservationDto.NumberOfPersons,
            Start = reservationDto.Start,
            End = reservationDto.End,
            Email = reservationDto.Email,
            FirstName = reservationDto.FirstName,
            LastName = reservationDto.LastName,
            Phone = reservationDto.Phone,
            Price = reservationDto.Price,
            Description = reservationDto.Description,
            Status = reservationDto.Status
        };

        var createdReservation = await _reservationRepository.AddAsync(reservation);
        return MapReservationToDto(createdReservation);
    }

    public async Task<ReservationDto> UpdateReservationAsync(ReservationDto reservationDto)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationDto.Id);
        if (reservation == null) return null;
        
        reservation.Status = reservationDto.Status;

        var updatedReservation = await _reservationRepository.UpdateAsync(reservation);
        return MapReservationToDto(updatedReservation ?? reservation);
    }

    public async Task<ReservationDto?> DeleteReservationAsync(Guid id)
    {
        var reservation = await _reservationRepository.DeleteAsync(id);
        return reservation == null ? null : MapReservationToDto(reservation);
    }

    private static ReservationDto MapReservationToDto(Reservation reservation)
    {
        return new ReservationDto()
        {
            Id = reservation.Id,
            NumberOfPersons = reservation.NumberOfPersons,
            Start = reservation.Start,
            End = reservation.End,
            Email = reservation.Email,
            FirstName = reservation.FirstName,
            LastName = reservation.LastName,
            Phone = reservation.Phone,
            Price = reservation.Price,
            Description = reservation.Description ?? String.Empty,
            Status = reservation.Status
        };
    }
}