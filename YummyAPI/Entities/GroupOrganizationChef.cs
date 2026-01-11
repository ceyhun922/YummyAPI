using System.ComponentModel.DataAnnotations;

namespace YummyAPI.Entities
{
    public class GroupOrganizationChef
    {   
        [Key]
        public int GroupOrganizationChefId { get; set; }
        public int GroupOrganizationId { get; set; }
        public GroupOrganization GroupOrganization { get; set; } = null!;
        public int ChefId { get; set; }
        public Chef? Chef { get; set; }
    }
}