using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string? ServiceIcon { get; set; }
        public string? ServiceTitle { get; set; }
        public string? ServiceDescription { get; set; }
        public bool ServiceStatus { get; set; }
    }
}