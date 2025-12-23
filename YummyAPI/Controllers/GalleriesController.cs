using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.GalleryDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleriesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public GalleriesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateGallery(CreateGalleryDto createGalleryDto)
        {
            var values = _mapper.Map<Gallery>(createGalleryDto);
            _context.Galleries?.Add(values);
            _context.SaveChanges();
            return Ok(new { message = "Eklendi" });
        }
        [HttpGet]
        public IActionResult GalleryList()
        {
            var values = _context.Galleries?.ToList();
            var mapper = _mapper.Map<List<ResultGalleryDto>>(values);
            return Ok(new { message = "Gallery list", data = mapper });
        }

        [HttpPut]
        public IActionResult UpdateGallery(UpdateGalleryDto updateGalleryDto)
        {
            var mapper = _mapper.Map<Gallery>(updateGalleryDto);
            _context.Galleries?.Update(mapper);
            _context.SaveChanges();
            return Ok(new { message = "Yenilendi" });
        }

        [HttpDelete]
        public IActionResult DeleteGallery(int id)
        {
            var value = _context.Galleries?.Find(id);

            if (value == null)
            {
                return Ok(new { message = "Tapılmadı" });
            }
            _context.Galleries?.Remove(value);
            _context.SaveChanges();
            return Ok(new { message = "Silindi" });
        }
    }
}