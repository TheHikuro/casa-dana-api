namespace CasaDanaAPI.DTOs.Calendars
{
    public class CalendarDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; } = 88;
    }
}