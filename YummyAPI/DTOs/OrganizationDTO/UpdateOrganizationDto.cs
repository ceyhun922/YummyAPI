namespace YummyAPI.DTOs.OrganizationDTO
{
    public class UpdateOrganizationDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }=string.Empty;
        public string? OrganizationDescription { get; set; }
        public double OrganizationPrice { get; set; }
        public bool OrganizationStatus { get; set; }
    }
}