using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.ServiceDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ServicesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateService(CreateServiceDto createServiceDto)
        {
            var mapper = _mapper.Map<Service>(createServiceDto);
            _context.Services?.Add(mapper);
            _context.SaveChanges();
            return Ok(mapper);
        }

        [HttpGet]
        public IActionResult ServiceList()
        {
            var values = _context.Services?.ToList();
            var mapper = _mapper.Map<List<ResultServiceDto>>(values);
            return Ok(mapper);
        }

        [HttpDelete]
        public IActionResult DeleteService(int id)
        {
            var value = _context.Services?.Find(id);

            if (value == null) return Ok();

            _context.Services?.Remove(value);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateService(UpdateService updateService)
        {
            var mapper = _mapper.Map<Service>(updateService);

            if (mapper == null)
            {
                return Ok(mapper);
            }
            _context.Services?.Update(mapper);
            _context.SaveChanges();
            return Ok(mapper);
        }
        [HttpGet("GetByIdService")]
        public IActionResult GetByIdService(int id)
        {
            var value =_context.Services?.Find(id);
            if(value == null)
                return Ok();
                
            var mapper =_mapper.Map<GetByIdServiceDto>(value);
            return Ok(mapper);
        }
    }
}