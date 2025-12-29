using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.AboutDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public AboutController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            var values =_context.Abouts?.ToList();
            var mapper =_mapper.Map<List<ResultAboutDto>>(values);
            return Ok(mapper);
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {
            var mapper =_mapper.Map<About>(createAboutDto);
            _context.Abouts?.Add(mapper);
            _context.SaveChanges();
            return Ok(new {message ="About added"});
        }
        
        [HttpDelete]
        public IActionResult DeleteAbout(int id)
        {
            var item =_context.Abouts?.Find(id);

            if (item == null)
            {
                return Ok(new {message="Melumat Tapılmadı"});
            }
            _context.Abouts?.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateAbout()
        {
            return Ok();
        }
        [HttpGet("GetByIdAboutArea")]
        public IActionResult GetByIdAboutArea(int id)
        {
            var value =_context.Abouts?.Find(id);
            var mapper =_mapper.Map<GetByIdAboutDto>(value);
            return Ok(mapper);
        }
    }
}