using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }=string.Empty;
        public string? OrganizationDescription { get; set; }
        public double OrganizationPrice { get; set; }
<<<<<<< HEAD
        public string? OrganizationImage { get; set; }
=======
>>>>>>> 6f2ea93 (Entities ve DTOs elave edildi)
        public bool OrganizationStatus { get; set; }
    }
}