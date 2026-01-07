namespace YummyUI.DTOs.RezervationDTOs
{
    public class ResultRezervationDto
    {
        public int RezervationId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly RezervationDate { get; set; }
        public TimeOnly RezervationClockk { get; set; }
        public int PersonCount { get; set; }
        public string? Message { get; set; }
        public int RezervationStatus { get; set; }



    }
}