using AutoMapper;
using CasaDanaAPI.DTOs.Calendars;
using CasaDanaAPI.Models.Calendar;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CasaDanaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CalendarController(ICalendarService calendarService, IMapper mapper) : ControllerBase
{
    [HttpGet("price")]
    public async Task<ActionResult<List<object>>> GetPriceForDateRange([FromQuery] string start, [FromQuery] string end)
    {
        if (string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(end))
            return BadRequest("Start and End dates are required.");

        try
        {
            var prices = await calendarService.GetPriceForDateRangeAsync(start, end);

            var response = prices.Select(p => new { Date = p.Date.ToString("dd/MM/yyyy"), Price = p.Price }).ToList();
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CalendarDto>>> GetAll()
    {
        var entries = await calendarService.GetAllCalendarEntriesAsync();
        return Ok(entries.Select(mapper.Map<CalendarDto>));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CalendarDto>> GetById(Guid id)
    {
        var entry = await calendarService.GetCalendarEntryByIdAsync(id);
        return entry is null ? NotFound() : Ok(mapper.Map<CalendarDto>(entry));
    }

    [HttpPost]
    public async Task<ActionResult<CalendarDto>> Create([FromBody] CalendarDto body)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var entry = mapper.Map<Calendar>(body);
        var created = await calendarService.CreateCalendarEntryAsync(entry);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, mapper.Map<CalendarDto>(created));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] CalendarDto body)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var entry = mapper.Map<Calendar>(body);
        var updated = await calendarService.UpdateCalendarEntryAsync(id, entry);

        return updated is null ? NotFound() : Ok(mapper.Map<CalendarDto>(updated));    
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await calendarService.DeleteCalendarEntryAsync(id);
        return NoContent();
    }
}
