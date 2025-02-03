using CasaDanaAPI.DTOs.Reservations;
using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CasaDanaAPI.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CasaDanaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        return Ok(reservations.Select(r => r.MapTo<ReservationDto>()));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(Guid id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();

        return Ok(reservation.MapTo<ReservationDto>());
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> CreateReservation([FromBody] ReservationDto createReservationDto)
    {
        if (createReservationDto is null) return BadRequest();

        var reservation = createReservationDto.MapTo<Reservation>();

        var createdReservation = await _reservationService.CreateReservationAsync(reservation);

        return CreatedAtAction(nameof(GetReservation), new { id = createdReservation.Id }, createdReservation.MapTo<ReservationDto>());
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ReservationDto>> UpdateReservation(Guid id, ReservationDto updateReservationDto)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();

        updateReservationDto.MapTo(reservation);
        var updatedReservation = await _reservationService.UpdateReservationAsync(reservation);

        return Ok(updatedReservation.MapTo<ReservationDto>());
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteReservation(Guid id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();

        await _reservationService.DeleteReservationAsync(id);
        return NoContent();
    }
}