namespace YummyAPI.DTOs.ServiceDTO
{
    public class CreateServiceDto
    {
        public string? ServiceIcon { get; set; }
        public string? ServiceTitle { get; set; }
        public string? ServiceDescription { get; set; }
        public bool ServiceStatus { get; set; }
    }
}