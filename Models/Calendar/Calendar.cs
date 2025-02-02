using System.ComponentModel.DataAnnotations;

namespace CasaDanaAPI.Models.Calendar
{
    public class Calendar
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int Price { get; set; } = 88;
    }
}