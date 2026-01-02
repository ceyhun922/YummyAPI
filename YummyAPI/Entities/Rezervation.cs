using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{
    public class Rezervation
    {
        [Key]
        public int RezervationId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly RezervationDate { get; set; }
        public TimeOnly RezervationClockk { get; set; }
        public int PersonCount { get; set; }
        public bool RezervationStatus { get; set; } = false;
    }
}