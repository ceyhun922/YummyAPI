namespace YummyAPI.DTOs.OrganizationDTO
{
    public class GetByIdOrganizationDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
    }
}