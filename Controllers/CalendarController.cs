using AutoMapper;
using CasaDanaAPI.DTOs.Calendars;
using CasaDanaAPI.Models.Calendar;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CasaDanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController(ICalendarService calendarService, IMapper mapper) : ControllerBase
    {
        [HttpGet("price")]
        public async Task<ActionResult> GetPriceForDate([FromQuery] DateTime date)
        {
            if (date == default) return BadRequest("Invalid date.");

            var price = await calendarService.GetPriceForDateAsync(date);
            return Ok(price);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var entries = await calendarService.GetAllCalendarEntriesAsync();
            return Ok(entries.Select(e => mapper.Map<CalendarDto>(e)));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var entry = await calendarService.GetCalendarEntryByIdAsync(id);
            if (entry is null) return NotFound();

            return Ok(mapper.Map<CalendarDto>(entry));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CalendarDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var calendar = mapper.Map<Calendar>(body);
            var created = await calendarService.CreateCalendarEntryAsync(calendar);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, mapper.Map<CalendarDto>(created));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] CalendarDto body)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var calendar = mapper.Map<Calendar>(body);
            await calendarService.UpdateCalendarEntryAsync(id, calendar);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await calendarService.DeleteCalendarEntryAsync(id);
            return NoContent();
        }
    }
}
