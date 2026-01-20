using YummyAPI.Entities;

namespace YummyAPI.DTOs.GroupOrganizationDTO
{
    public class CreateGroupOrganizationDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public GroupPriority GroupPriority { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public int PersonCount { get; set; }
        public int ParticipantCount { get; set; }
        public int ParticipationRate { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public List<int> ChefIds { get; set; }
    }
}