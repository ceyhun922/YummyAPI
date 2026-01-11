using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{
    public class GroupOrganization
    {
        [Key]
        public int GroupOrganizationId { get; set; }
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public GroupPriority GroupPriority { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public int PersonCount { get; set; } = 50;
        public int ParticipantCount { get; set; } =0;
        public int ParticipationRate
        {
            get
            {
                if (PersonCount <= 0) return 0;
                return (int) Math.Round((double)ParticipantCount *100) / PersonCount;

            }
        }

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        List<GroupOrganizationChef> GroupOrganizationChefs { get; set; } = new();




    }
}