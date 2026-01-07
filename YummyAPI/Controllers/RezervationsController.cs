using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.RezervationDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RezervationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public RezervationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult RezervationList()
        {
            var values = _context.Rezervations?.ToList();
            var mapper = _mapper.Map<List<ResultRezervationDto>>(values);

            return Ok(mapper);
        }

        [HttpPost]
        public IActionResult RezervationCreate(CreateRezervationDto createRezervationDto)
        {
            var mapper = _mapper.Map<Rezervation>(createRezervationDto);

            _context.Rezervations?.Add(mapper);
            _context.SaveChanges();

            return Ok(mapper);
        }

        [HttpDelete]
        public IActionResult RezervationDelete(int id)
        {
            var value = _context.Rezervations?.Find(id);
            if (value == null)
            {
                return Ok();
            }

            _context.Rezervations?.Remove(value);
            _context.SaveChanges();

            return Ok();

        }

        [HttpPut]
        public IActionResult RezervationUpdate(UpdateRezervationDto updateRezervationDto)
        {
            var mapper = _mapper.Map<Rezervation>(updateRezervationDto);

            if (mapper == null)
            {
                return Ok(mapper);
            }
            _context.Rezervations?.Update(mapper);
            _context.SaveChanges();

            return Ok(mapper);
        }

        [HttpGet("ApprovedRezervastion")]
        public IActionResult ApprovedRezervastion()
        {
            var value = _context.Rezervations?.Where(x => x.RezervationStatus == RezervationStatus.Approved).ToList(); ;
            return Ok(value);
        }

        [HttpPut("ChangeStatus/{id}")]
        public IActionResult ChangeStatus(int id, [FromQuery] string status)
        {
            var rez = _context.Rezervations?.Find(id);
            if (rez == null) return NotFound();

            var map = new Dictionary<string, RezervationStatus>(StringComparer.OrdinalIgnoreCase)
            {
                ["Approved"] = RezervationStatus.Approved,
                ["Pending"] = RezervationStatus.OnHold,
                ["Canceled"] = RezervationStatus.Canceled
            };

            rez.RezervationStatus = map.TryGetValue(status, out var newStatus)
                ? newStatus
                : RezervationStatus.Pending;

            _context.SaveChanges();

            // istəsən entity qaytar:
            // return Ok(rez);

            // daha yaxşısı: DTO qaytar
            var dto = _mapper.Map<ResultRezervationDto>(rez);
            return Ok(dto);
        }

    }
}