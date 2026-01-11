using YummyUI.DTOs.OrganizationDTOs;

namespace YummyUI.DTOs.GroupOrganizationDTO
{
    public class ResultGroupOrganizationDto
    {
        public int GroupOrganizationId { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty; 
        public int GroupPriority { get; set; }
        public int PersonCount { get; set; }
        public int ParticipantCount { get; set; } 
        public string? ParticipationRate { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public ResultOrganizationDto? ResultOrganizationDto { get; set; } = new();
    }
}