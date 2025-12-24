namespace YummyUI.DTOs.ServiceDTOs
{
    public class ResultServiceDto
    {
        public int ServiceId { get; set; }
        public string? ServiceIcon { get; set; }
        public string? ServiceTitle { get; set; }
        public string? ServiceDescription { get; set; }
        public bool ServiceStatus { get; set; }
    }
}