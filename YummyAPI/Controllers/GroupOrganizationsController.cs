using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YummyAPI.Context;
using YummyAPI.DTOs.GroupOrganizationDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupOrganizationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public GroupOrganizationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GroupOrganizationsList()
        {
            var values = _context.GroupOrganizations.
            Include(x => x.GroupOrganizationChefs)
            .ThenInclude(x => x.Chef)
            .ToList();

            if (values == null)
            {
                return Ok(new { message = "Boşdur" });
            }

            var mapper = _mapper.Map<List<ResultGroupOrganizationDto>>(values);

            if (mapper == null)
            {
                return Ok(new { message = "Boşdur" });
            }

            foreach (var dto in mapper)
            {
                var entity = values.First(x => x.GroupOrganizationId == dto.GroupOrganizationId);

                dto.ChefIds = entity.GroupOrganizationChefs.Select(x => x.ChefId).ToList();
            }

            return Ok(mapper);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupOrganizations(CreateGroupOrganizationDto createGroupOrganizationDto)
        {
            var mapper = _mapper.Map<GroupOrganization>(createGroupOrganizationDto);

            await _context.GroupOrganizations.AddAsync(mapper);
            await _context.SaveChangesAsync();

            if (createGroupOrganizationDto.ChefIds != null && createGroupOrganizationDto.ChefIds.Count > 0)
            {
                var chefIds = createGroupOrganizationDto.ChefIds.Distinct().ToList();

                var rows = chefIds.Select(chefId => new GroupOrganizationChef
                {
                    GroupOrganizationId = mapper.GroupOrganizationId,
                    ChefId = chefId
                }).ToList();

                _context.GroupOrganizationChefs.AddRange(rows);
                _context.SaveChanges();
            }
            return Ok(new { message = "Eklendi", id = mapper.GroupOrganizationId });
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveGroupOrganization(int id)
        {
            var value = await _context.GroupOrganizations
                .Include(x => x.GroupOrganizationChefs)
                .FirstOrDefaultAsync(x => x.GroupOrganizationId == id);

            if (value == null) return NotFound();

            _context.GroupOrganizations.Remove(value);
            _context.GroupOrganizationChefs.RemoveRange(value.GroupOrganizationChefs);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroupOrganization([FromQuery] int id, [FromBody] JsonElement body)
        {
            var dto = JsonSerializer.Deserialize<UpdateGroupOrganizationDto>(body.GetRawText(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (dto == null) return BadRequest("Dto boş");

            var value = await _context.GroupOrganizations
                .Include(x => x.GroupOrganizationChefs)
                .FirstOrDefaultAsync(x => x.GroupOrganizationId == id);

            if (value == null) return NotFound();

            _mapper.Map(dto, value);

            var newChef = (dto.ChefIds ?? new List<int>()).Distinct().ToHashSet();
            var oldChef = value.GroupOrganizationChefs.Select(x => x.ChefId).ToHashSet();

            var toRemove = value.GroupOrganizationChefs.Where(x => !newChef.Contains(x.ChefId)).ToList();
            var toAdd = newChef.Where(chefId => !oldChef.Contains(chefId))
                .Select(chefId => new GroupOrganizationChef
                {
                    GroupOrganizationId = value.GroupOrganizationId,
                    ChefId = chefId
                }).ToList();

            if (toRemove.Count > 0) _context.GroupOrganizationChefs.RemoveRange(toRemove);
            if (toAdd.Count > 0) await _context.GroupOrganizationChefs.AddRangeAsync(toAdd);

            await _context.SaveChangesAsync();

            return Ok(new { id = value.GroupOrganizationId });
        }

       [HttpGet("{id:int}")]
public async Task<IActionResult> UpdateGroupOrganization(int id)
{
    var data = await _context.GroupOrganizations
        .Where(x => x.GroupOrganizationId == id)
        .Select(x => new
        {
            groupOrganizationId = x.GroupOrganizationId,

            organizationId = x.OrganizationId,
            organizationName = x.Organization,

            groupPriority = x.GroupPriority,
            price = x.Price,
            description = x.Description,

            personCount = x.PersonCount,
            participantCount = x.ParticipantCount,
            participationRate = x.ParticipationRate,

            date = x.Date,     
            time = x.Time,     

            chefIds = x.GroupOrganizationChefs
                .Select(gc => gc.ChefId)
                .Distinct()
                .ToList()
        })
        .FirstOrDefaultAsync();

    if (data == null) return NotFound();

    return Ok(data);
}




    }
}