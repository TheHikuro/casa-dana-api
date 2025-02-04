using System.ComponentModel.DataAnnotations;

namespace CasaDanaAPI.Models.Calendar
{
    public class Calendar
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public required DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public required DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(1);

        [Required]
        public required int Price { get; set; } = 88;
    }
}