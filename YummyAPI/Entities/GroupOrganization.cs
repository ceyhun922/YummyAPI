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

        private int _personCount = 50;

        public int PersonCount
        {
            get => _personCount;
            set
            {
                if (value > 50)
                    _personCount = 50;
                else if (value < 0)
                    _personCount = 0;
                else
                    _personCount = value;
            }
        }


        public int ParticipantCount { get; set; } = 0;
        public int ParticipationRate
        {
            get
            {
                if (PersonCount <= 0) return 0;
                var rate = (int)Math.Round((double)ParticipantCount * 100) / PersonCount;
                if (rate > 100) return 100;
                if (rate < 0) return 0;

                return rate;

            }
        }

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public List<GroupOrganizationChef> GroupOrganizationChefs { get; set; } = new();

    }
}