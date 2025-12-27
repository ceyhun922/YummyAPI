namespace YummyAPI.DTOs.NotificationDTO
{
    public class ResultNotificationReadFalseDto
    {
        public int NotificationId { get; set; }
        public string? NotificationTitle { get; set; }
        public string? NotificationMessage { get; set; }
        public string? NotificationIcon { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}