using AutoMapper;
using CasaDanaAPI.DTOs.Reservations;
using CasaDanaAPI.Enums;
using CasaDanaAPI.Models.Reservations;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace CasaDanaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController(IReservationService reservationService, IMapper mapper, IEmailService emailService, IConfiguration configuration) : ControllerBase
{
    private readonly string _templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Infrastructure", "Configuration", "Templates");
    private readonly string _senderEmail = configuration["EmailSettings:SenderEmail"]!; 

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
        
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Paris");
        var localStart = TimeZoneInfo.ConvertTimeFromUtc(body.Start, timeZone);
        var localEnd = TimeZoneInfo.ConvertTimeFromUtc(body.End, timeZone);

        string guestEmailBody = LoadTemplate("GuestEmail.html")
            .Replace("{{FirstName}}", body.FirstName)
            .Replace("{{LastName}}", body.LastName)
            .Replace("{{StartDate}}", localStart.ToString("dd/MM/yyyy"))
            .Replace("{{EndDate}}", localEnd.ToString("dd/MM/yyyy"));

        string hostEmailBody = LoadTemplate("NewBookingNotification.html")
            .Replace("{{FirstName}}", body.FirstName)
            .Replace("{{LastName}}", body.LastName)
            .Replace("{{Email}}", body.Email)
            .Replace("{{StartDate}}", body.Start.ToString("dd/MM/yyyy"))
            .Replace("{{EndDate}}", body.End.ToString("dd/MM/yyyy"));
        
        await emailService.SendEmailAsync(
            to: body.Email,
            subject: "Votre demande de réservation",
            htmlMessage: guestEmailBody
        );
        
        await emailService.SendEmailAsync(
            to: _senderEmail,
            subject: "Nouvelle réservation reçue",
            htmlMessage: hostEmailBody
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

        if (status == ReservationStatus.Confirmed) 
        {
            var confirmationEmailBody = LoadTemplate("ReservationConfirmed.html")
                .Replace("{{FirstName}}", reservation.FirstName)
                .Replace("{{LastName}}", reservation.LastName)
                .Replace("{{StartDate}}", reservation.Start.ToString("dd/MM/yyyy"))
                .Replace("{{EndDate}}", reservation.End.ToString("dd/MM/yyyy"));

            await emailService.SendEmailAsync(
                to: reservation.Email,
                subject: "Confirmation de votre réservation",
                htmlMessage: confirmationEmailBody
            );
        }

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
    
    private string LoadTemplate(string fileName)
    {
        var filePath = Path.Combine(_templatePath, fileName);
    
        if (!System.IO.File.Exists(filePath))
        {
            Console.WriteLine($"⚠️ Template introuvable : {filePath}");
            return $"<p>Template {fileName} introuvable</p>";
        }

        return System.IO.File.ReadAllText(filePath);
    }

}
