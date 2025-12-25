using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.OrganizationDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrganizationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public OrganizationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult OrganizationList()
        {
            var values = _context.Organizations?.ToList();
            var mapper = _mapper.Map<List<ResultOrganizationDto>>(values);

            return Ok(mapper);
        }

        [HttpPost]
        public IActionResult OrganizationCreate(CreateOrganizationDto createOrganizationDto)
        {
            var mapper = _mapper.Map<Organization>(createOrganizationDto);

            _context.Organizations?.Add(mapper);
            _context.SaveChanges();

            return Ok(new { message = "Eklendi" });
        }

        [HttpDelete]
        public IActionResult OrganizationDelete(int id)
        {
            var value = _context.Organizations?.Find(id);
            if (value == null)
            {
                return Ok(new { message = "tapılmadı" });
            }

            _context.Organizations?.Remove(value);
            _context.SaveChanges();

            return Ok(new { message = "Silindi" });

        }

        [HttpPut]
        public IActionResult OrganizationUpdate(UpdateOrganizationDto updateOrganizationDto)
        {
            var mapper = _mapper.Map<Organization>(updateOrganizationDto);

            if (mapper == null)
            {
                return Ok(new { message = "tapılmadı" });
            }
            _context.Organizations?.Update(mapper);
            _context.SaveChanges();

            return Ok(new { message = "Yenilendi" });
        }
    }
}