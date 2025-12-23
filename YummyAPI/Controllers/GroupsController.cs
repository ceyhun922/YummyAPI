using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YummyAPI.Context;
using YummyAPI.DTOs.GroupOrganizationChefDTO;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public GroupsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("groups/organizations/chefs")]
        public async Task<IActionResult> GroupOrganizationWithChefs()
        {
            var data = await _context.GroupOrganizationChefs
                .Include(x => x.Chef)
                .Include(x => x.GroupOrganization)
                    .ThenInclude(go => go.Organization)
                .Select(x => new ResultGroupOrganizationChefDto
                {
                    GroupOrganizationChefId = x.GroupOrganizationChefId,
                    ChefId = x.ChefId,
                    ChefName = x.Chef.ChefName,
                    ImageFile = x.Chef.ImageFile,
                    GroupOrganizationId = x.GroupOrganizationId,
                    OrganizationName = x.GroupOrganization.Organization.OrganizationName,
                    ParticipantCount = x.GroupOrganization.ParticipantCount,
                    ParticipationRate = x.GroupOrganization.ParticipationRate,
                    Date = x.GroupOrganization.Date,
                    Time = x.GroupOrganization.Time,
                    GroupPriority = (int)x.GroupOrganization.GroupPriority
                })
                .ToListAsync();


            var dto = _mapper.Map<List<ResultGroupOrganizationChefDto>>(data);


            return Ok(dto);
        }


    }
}