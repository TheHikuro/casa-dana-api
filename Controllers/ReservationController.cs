using AutoMapper;
using CasaDanaAPI.DTOs.Reservations;
using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CasaDanaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IMapper _mapper;

    public ReservationsController(IReservationService reservationService, IMapper mapper)
    {
        _reservationService = reservationService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        var reservationResponseDto = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        return new OkObjectResult(reservationResponseDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(Guid id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null)
        {
            return new NotFoundResult();
        }
        var reservationResponseDto = _mapper.Map<ReservationDto>(reservation);
        return new OkObjectResult(reservationResponseDto);
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> CreateReservation(ReservationDto createReservationDto)
    {
        var createdReservation = await _reservationService.CreateReservationAsync(createReservationDto);
        var reservationResponseDto = _mapper.Map<ReservationDto>(createdReservation);
        return CreatedAtAction(nameof(GetReservation), new { id = reservationResponseDto.Id }, reservationResponseDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReservationDto>> UpdateReservation(Guid id, ReservationDto updateReservationDto)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null)
        {
            return new NotFoundResult();
        }
        _mapper.Map(updateReservationDto, reservation);
        var updatedReservation = await _reservationService.UpdateReservationAsync(reservation);
        var reservationResponseDto = _mapper.Map<ReservationDto>(updatedReservation);
        return new OkObjectResult(reservationResponseDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReservation(Guid id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null)
        {
            return new NotFoundResult();
        }
        await _reservationService.DeleteReservationAsync(id);
        return new NoContentResult();
    }
}