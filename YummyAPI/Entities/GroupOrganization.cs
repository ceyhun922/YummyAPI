namespace YummyAPI.Entities
{
    public class GroupOrganization
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public int ChefId { get; set; }
        public string? ChefName { get; set; }
        public GroupPriority GroupPriority {get;set;}
        public double? Price { get; set; }
        public string? Description { get; set; }
        public string? ParticipationRate { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        List<GroupOrganizationChef>? GroupOrganizationChefs {get;set;}




    }
}