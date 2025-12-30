namespace YummyUI.DTOs.ServiceDTOs
{
    public class GetByIdServiceDto
    {
        public int ServiceId { get; set; }
        public string? ServiceIcon { get; set; }
        public string? ServiceTitle { get; set; }
        public string? ServiceDescription { get; set; }
        public bool ServiceStatus { get; set; }
    }
}