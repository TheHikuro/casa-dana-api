using AutoMapper;
using CasaDanaAPI.DTOs.Reservations;
using CasaDanaAPI.Enums;
using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CasaDanaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController(IReservationService reservationService, IMapper mapper, IEmailService emailService) : ControllerBase
{
    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
    {
        var reservations = await reservationService.GetAllReservationsAsync();
        return Ok(reservations.Select(mapper.Map<ReservationDto>));    
    }

    [HttpGet("{id:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async Task<ActionResult<ReservationDto>> GetReservation(Guid id)
    {
        var reservation = await reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();

        return Ok(mapper.Map<ReservationDto>(reservation));
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    public async Task<ActionResult<ReservationDto>> CreateReservation([FromBody] ReservationDto body)
    {
        var reservation = mapper.Map<Reservation>(body);
        var createdReservation = await reservationService.CreateReservationAsync(reservation);

        await emailService.SendEmailAsync(
            to: body.Email,
            subject: "Reservation Confirmation",
            htmlMessage:
                $"<p>Bonjour {body.FirstName} {body.LastName}, Merci pour votre reservation ! Nous reviendrons rapidement vers vous !</p>"
            );

        return CreatedAtAction(nameof(GetReservation), new { id = createdReservation.Id }, mapper.Map<ReservationDto>(createdReservation));
    }

    [Authorize]
    [HttpPatch("{id:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Update))]
    public async Task<ActionResult<ReservationDto>> UpdateReservation(Guid id, ReservationStatus status)
    {
        var reservation = await reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();
        
        var updatedReservation = await reservationService.UpdateReservationAsync(id, status);

        return Ok(mapper.Map<ReservationDto>(updatedReservation));
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<ActionResult> DeleteReservation(Guid id)
    {
        var reservation = await reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();

        await reservationService.DeleteReservationAsync(id);
        return NoContent();
    }
}
