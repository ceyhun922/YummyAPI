namespace YummyUI.DTOs.OrganizationDTOs
{
    public class CreateOrganizationDto
    {
        public string OrganizationName { get; set; } = string.Empty;
        public string? OrganizationDescription { get; set; }
        public double OrganizationPrice { get; set; }
        public string? OrganizationImage { get; set; }
        public bool OrganizationStatus { get; set; }
    }
}