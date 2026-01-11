using YummyUI.DTOs.ChefDTOs;
using YummyUI.DTOs.GroupOrganizationDTO;

namespace YummyUI.DTOs.GroupOrganizationChefDTO
{
    public class ResultGroupOrganizationChefDto
    {
        public int GroupOrganizationChefId { get; set; }
        public int GroupOrganizationId { get; set; }
        public int ChefId { get; set; }

        public ResultGroupOrganizationDto? GroupOrganization { get; set; }
        public ResultChefDto? Chef { get; set; }
    }
}