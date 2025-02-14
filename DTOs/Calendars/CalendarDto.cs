using System.ComponentModel.DataAnnotations;

namespace CasaDanaAPI.DTOs.Calendars
{
    public class CalendarDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Une date de début est requise.")]
        public required DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "Une date de fin est requise.")]
        public required DateTime EndDate { get; set; }
         
        [Required(ErrorMessage = "Un prix est requis.")]
        public required int Price { get; set; } = 90;
    }
}