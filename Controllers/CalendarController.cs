using CasaDanaAPI.DTOs.Calendars;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CasaDanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        /// <summary>
        /// Returns the effective price for a given date.
        /// Example: GET /api/Calendar/price?date=2025-01-01
        /// </summary>
        [HttpGet("price")]
        public async Task<IActionResult> GetPriceForDate([FromQuery] DateTime date)
        {
            if (date == default)
            {
                return BadRequest("Invalid date.");
            }

            var price = await _calendarService.GetPriceForDateAsync(date);
            return Ok(price);
        }

        /// <summary>
        /// Return all calendar entries (custom date ranges with prices).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entries = await _calendarService.GetAllCalendarEntriesAsync();
            return Ok(entries);
        }

        /// <summary>
        /// Get a specific CalendarEntry by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var entry = await _calendarService.GetCalendarEntryByIdAsync(id);
            if (entry == null)
                return NotFound();

            return Ok(entry);
        }

        /// <summary>
        /// Create a new CalendarEntry (date range + price).
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CalendarDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _calendarService.CreateCalendarEntryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Update an existing CalendarEntry by ID.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CalendarDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _calendarService.UpdateCalendarEntryAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Delete a CalendarEntry by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _calendarService.DeleteCalendarEntryAsync(id);
            return NoContent();
        }
    }
}
