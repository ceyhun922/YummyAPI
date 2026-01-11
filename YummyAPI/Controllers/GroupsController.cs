using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YummyAPI.Context;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ApiContext _context;

        public GroupsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GroupOrganization()
        {
            var values = _context.GroupOrganizations?.ToList();
            return Ok(values);
        }

        [HttpGet("groups/organizations/chefs")]
        public IActionResult GroupOrganizationWithChefs()
        {
            var values = _context.GroupOrganizationChefs
                .Include(x => x.Chef)
                .Include(x => x.GroupOrganization)
                    .ThenInclude(g => g.Organization)
                .ToList();

            return Ok(values);
        }


    }
}