namespace YummyUI.DTOs.NotificationDTOs
{
    public class ResultNotificationReadFalseDto
    {
        public string? NotificationTitle { get; set; }
        public string? NotificationMessage { get; set; }
        public string? NotificationIcon { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }=false;
    }
}