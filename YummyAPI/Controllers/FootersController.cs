using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.FooterDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootersController : ControllerBase
    {
         private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public FootersController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult FooterList()
        {
            var values = _context.Footers?.ToList();
            var mapper = _mapper.Map<List<ResultFooterDto>>(values);

            return Ok(mapper);
        }

        [HttpPost]
        public IActionResult FooterCreate(CreateFooterDto createFooterDto)
        {
            var mapper = _mapper.Map<Footer>(createFooterDto);

            _context.Footers?.Add(mapper);
            _context.SaveChanges();

            return Ok(new { message = "Eklendi" });
        }

        [HttpDelete]
        public IActionResult FooterDelete(int id)
        {
            var value = _context.Footers?.Find(id);
            _context.Footers?.Remove(value);
            _context.SaveChanges();

            return Ok(new {message ="Silindi"});

        }

        [HttpPut]
        public IActionResult FooterUpdate(UpdateFooterDto updateFooterDto)
        {
            var mapper =_mapper.Map<Footer>(updateFooterDto);
            _context.Footers?.Update(mapper);
            _context.SaveChanges();

            return Ok(new {message ="Yenilendi"});
        }
    }
}