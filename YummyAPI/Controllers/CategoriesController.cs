using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.CategoryDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var values = _mapper.Map<Category>(createCategoryDto);
            _context.Categories?.Add(values);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            var values = _context.Categories?.ToList();
            var mapper = _mapper.Map<List<ResultCategoryDTOs>>(values);
            return Ok(mapper);
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDTOs updateCategoryDTOs)
        {
            var values = _mapper.Map<Category>(updateCategoryDTOs);
            _context.Categories?.Update(values);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categories?.Find(id);

            if (value == null)
            {
                return Ok();
            }

            _context.Categories?.Remove(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdCategory(int id)
        {
            var value = _context.Categories?.Find(id);
            if (value == null)
            {
                return Ok(new { message = "Bulunamadı" });
            }
            var mapper = _mapper.Map<GetCategoryByIdDto>(value);
            return Ok(mapper);
        }


        [HttpPost("toggle-status/{id:int}")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var value = await _context.Categories.FindAsync(id);
            if (value == null)
                return NotFound(new { success = false, message = "Kategori bulunamadı." });

            value.CategoryStatus = !value.CategoryStatus;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                status = value.CategoryStatus,
                message = value.CategoryStatus ? "Kategori aktif edildi." : "Kategori pasife edildi."
            });
        }


    }
}