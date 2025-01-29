using System.ComponentModel.DataAnnotations;

namespace CasaDanaAPI.Models
{
    public class Calendar
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<DateTime> Dates { get; set; } = new List<DateTime>();
        public decimal Price { get; set; }
    }
}