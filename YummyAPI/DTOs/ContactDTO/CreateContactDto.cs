namespace YummyAPI.DTOs.ContactDTO
{
    public class CreateContactDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public DateTime GetDate { get; set; }=DateTime.Now;
        public bool IsRead { get; set; } = false;

    }
}