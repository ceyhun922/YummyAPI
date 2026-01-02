using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.TestimonialDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestimonialsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public TestimonialsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult TestimonialList()
        {
            var values = _context.Testimonials?.ToList();
            var mapper = _mapper.Map<List<ResultTestimonialDto>>(values);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdTestimonial(int id)
        {
            var values = _context.Testimonials?.Find(id);
            var mapper = _mapper.Map<GetByIdTestimonialDto>(values);
            return Ok(mapper);
        }

        [HttpPost]
        public IActionResult TestimonialCreate(CreateTestimonialDto createTestimonialDto)
        {
            var mapper = _mapper.Map<Testimonial>(createTestimonialDto);

            _context.Testimonials?.Add(mapper);
            _context.SaveChanges();

            return Ok(mapper);
        }

        [HttpDelete]
        public IActionResult TestimonialDelete(int id)
        {
            var value = _context.Testimonials?.Find(id);
            if (value == null)
            {
                return Ok();
            }

            _context.Testimonials?.Remove(value);
            _context.SaveChanges();

            return Ok();

        }

        [HttpPut]
        public IActionResult TestimonialUpdate(UpdateTestimonialDto updateTestimonialDto)
        {
            var mapper = _mapper.Map<Testimonial>(updateTestimonialDto);

            if (mapper == null)
            {
                return Ok(mapper);
            }
            _context.Testimonials?.Update(mapper);
            _context.SaveChanges();

            return Ok(mapper);
        }
    }
}