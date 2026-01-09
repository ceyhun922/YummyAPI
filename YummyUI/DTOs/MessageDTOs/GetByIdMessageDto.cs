namespace YummyUI.DTOs.MessageDTOs
{
    public class GetByIdMessageDto
    {
        public int ContactId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public DateTime GetDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}