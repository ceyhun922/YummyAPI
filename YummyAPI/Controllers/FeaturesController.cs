using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.FeatureDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeaturesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public FeaturesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult FeatureCover()
        {
            var value = _context.Features?.ToList();
            var mapper = _mapper.Map<List<ResultFeatureDto>>(value);

            return Ok(mapper);
        }

        [HttpPost]
        public IActionResult FeatureCreate(CreateFeatureDto createFeatureDto)
        {
            var mapper = _mapper.Map<Feature>(createFeatureDto);

            _context.Features?.Add(mapper);
            _context.SaveChanges();

            return Ok(new { message = "Eklendi" });
        }

        [HttpDelete]
        public IActionResult FeatureDelete(int id)
        {
            var value = _context.Features?.Find(id);
            _context.Features?.Remove(value);
            _context.SaveChanges();

            return Ok(new {message ="Silindi"});

        }

        [HttpPut]
        public IActionResult FeatureUpdate(UpdateFeatureDto updateFeatureDto)
        {
            var mapper =_mapper.Map<Feature>(updateFeatureDto);
            _context.Features?.Update(mapper);
            _context.SaveChanges();

            return Ok(new {message ="Yenilendi"});
        }

        [HttpGet("GetByIdFeatureArea")]
        public IActionResult GetByIdFeatureArea(int id)
        {
            var value =_context.Features?.Find(id);
            if(value == null)
                return Ok(new {message ="Tapılmadı"});
            var mapper =_mapper.Map<GetByIdFeatureDto>(value);
            return Ok(mapper);
        }
    }
}