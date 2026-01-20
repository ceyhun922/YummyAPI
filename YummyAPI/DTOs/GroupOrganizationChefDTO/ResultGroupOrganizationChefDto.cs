namespace YummyAPI.DTOs.GroupOrganizationChefDTO
{
    public class ResultGroupOrganizationChefDto
{
    public int GroupOrganizationChefId { get; set; }
    public int ChefId { get; set; }
    public string ChefName { get; set; }
    public string ImageFile { get; set; }

    public int GroupOrganizationId { get; set; }
    public string OrganizationName { get; set; }

    public int ParticipationRate { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public int GroupPriority { get; set; }
}

}