using AutoMapper;
using CasaDanaAPI.DTOs.Reservations;
using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CasaDanaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(IReservationService reservationService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
    {
        var reservations = await reservationService.GetAllReservationsAsync();
        return Ok(reservations.Select(r => mapper.Map<ReservationDto>(r)));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(Guid id)
    {
        var reservation = await reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();

        return Ok(mapper.Map<ReservationDto>(reservation));
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> CreateReservation([FromBody] ReservationDto body)
    {
        if (body is null) return BadRequest();

        var reservation = mapper.Map<Reservation>(body);

        var createdReservation = await reservationService.CreateReservationAsync(reservation);

        return CreatedAtAction(nameof(GetReservation), new { id = createdReservation.Id }, mapper.Map<ReservationDto>(createdReservation));
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ReservationDto>> UpdateReservation(Guid id, ReservationDto updateReservationDto)
    {
        var reservation = await reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();

        mapper.Map(updateReservationDto, reservation);
        var updatedReservation = await reservationService.UpdateReservationAsync(reservation);

        return Ok(mapper.Map<ReservationDto>(updatedReservation));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteReservation(Guid id)
    {
        var reservation = await reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();

        await reservationService.DeleteReservationAsync(id);
        return NoContent();
    }
}
