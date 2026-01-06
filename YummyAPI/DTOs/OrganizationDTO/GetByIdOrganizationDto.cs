namespace YummyAPI.DTOs.OrganizationDTO
{
    public class GetByIdOrganizationDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string? OrganizationDescription { get; set; }
        public double OrganizationPrice { get; set; }
        public string? OrganizationImage { get; set; }
        public bool OrganizationStatus { get; set; }

    }
}