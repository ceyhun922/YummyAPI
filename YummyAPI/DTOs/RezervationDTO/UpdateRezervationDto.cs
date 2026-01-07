using YummyAPI.Entities;

namespace YummyAPI.DTOs.RezervationDTO
{
    public class UpdateRezervationDto
    {
        public int RezervationId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public TimeOnly Date { get; set; }
        public DateOnly Clock { get; set; }
        public int PersonCount { get; set; }
        public RezervationStatus RezervationStatus { get; set; }


    }
}