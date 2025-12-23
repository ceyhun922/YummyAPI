namespace YummyUI.DTOs.GroupOrganizationDTO
{
    public class CreateGroupOrganizationDto
    {
        public int OrganizationId { get; set; }
        public int GroupPriority { get; set; }
        public int PersonCount { get; set; }
        public int ParticipantCount { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public List<int> ChefIds { get; set; } = new();
    }
}