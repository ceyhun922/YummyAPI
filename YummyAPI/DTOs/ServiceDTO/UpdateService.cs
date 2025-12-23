namespace YummyAPI.DTOs.ServiceDTO
{
    public class UpdateService
    {
        public int ServiceId { get; set; }
        public string? ServiceIcon { get; set; }
        public string? ServiceTitle { get; set; }
        public string? ServiceDescription { get; set; }
        public bool ServiceStatus { get; set; }
    }
}