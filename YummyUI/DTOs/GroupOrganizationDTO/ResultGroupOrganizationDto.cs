using YummyUI.DTOs.OrganizationDTOs;

public class ResultGroupOrganizationDto
{
    public int GroupOrganizationId { get; set; }
    public int OrganizationId { get; set; }
    public string OrganizationName { get; set; } = string.Empty;
    public int GroupPriority { get; set; }
    public int PersonCount { get; set; }
    public int ParticipantCount { get; set; }
    public int ParticipationRate { get; set; }  
    public string Date { get; set; } = "";
    public string Time { get; set; } = "";
    public ResultOrganizationDto? ResultOrganizationDto { get; set; } = new();
}