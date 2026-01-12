
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.ChefDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _web;

        public ChefsController(ApiContext context, IMapper mapper, IWebHostEnvironment web)
        {
            _context = context;
            _mapper = mapper;
            _web = web;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChef([FromForm] CreateChefDto dto)
        {
            var entity = _mapper.Map<Chef>(dto);

            _context.Chefs.Add(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }



        [HttpGet]
        public IActionResult ChefList()
        {
            var values = _context.Chefs?.ToList();
            var mapper = _mapper.Map<List<ResultChefDto>>(values);
            return Ok(mapper);
        }


        [HttpPut]
        public IActionResult UpdateChef(UpdateChefDto updateChefDto)
        {
            var mapper = _mapper.Map<Chef>(updateChefDto);
            _context.Chefs?.Update(mapper);
            _context.SaveChanges();

            return Ok(mapper);
        }

        [HttpDelete]
        public IActionResult DeleteChef(int id)
        {
            var value = _context.Chefs?.Find(id);

            if (value == null)
            {
                return Ok();
            }

            _context.Chefs?.Remove(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("GetOneChef")]
        public IActionResult GetOneChef(int id)
        {
            var value = _context.Chefs?.Find(id);
            if (value == null)
            {
                return Ok();
            }
            var mapper = _mapper.Map<GetByIdChefDto>(value);

            return Ok(mapper);
        }
    }
}