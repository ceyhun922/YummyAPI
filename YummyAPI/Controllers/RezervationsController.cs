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

            return Ok(new { message = "Rezervation List", data = mapper });
        }

        [HttpPost]
        public IActionResult RezervationCreate(CreateRezervationDto createRezervationDto)
        {
            var mapper = _mapper.Map<Rezervation>(createRezervationDto);

            _context.Rezervations?.Add(mapper);
            _context.SaveChanges();

            return Ok(new { message = "Eklendi" });
        }

        [HttpDelete]
        public IActionResult RezervationDelete(int id)
        {
            var value = _context.Rezervations?.Find(id);
            if (value == null)
            {
                return Ok(new { message = "tapılmadı" });
            }

            _context.Rezervations?.Remove(value);
            _context.SaveChanges();

            return Ok(new { message = "Silindi" });

        }

        [HttpPut]
        public IActionResult RezervationUpdate(UpdateRezervationDto updateRezervationDto)
        {
            var mapper = _mapper.Map<Rezervation>(updateRezervationDto);

            if (mapper == null)
            {
                return Ok(new { message = "tapılmadı" });
            }
            _context.Rezervations?.Update(mapper);
            _context.SaveChanges();

            return Ok(new { message = "Yenilendi" });
        }
    }
}