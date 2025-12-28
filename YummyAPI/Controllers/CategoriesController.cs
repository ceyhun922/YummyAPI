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
            return Ok(new { message = "Category Eklendi" });
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            var values =_context.Categories?.ToList();
            var mapper = _mapper.Map<List<ResultCategoryDTOs>>(values);
            return Ok(mapper);
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDTOs updateCategoryDTOs)
        {
            var values =_mapper.Map<Category>(updateCategoryDTOs);
            _context.Categories?.Update(values);
            _context.SaveChanges();
            return Ok(new {message ="Yenilendi"});
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var value =_context.Categories?.Find(id);

            if (value == null)
            {
                return Ok(new {message="Category Tapılmadı"});
            }

            _context.Categories?.Remove(value);
            _context.SaveChanges();
            return Ok(new {message="Category Silindi"});
        }

        [HttpGet("GetCategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            var value =_context.Categories?.Find(id);
            if (value == null)
            {
                return Ok(new {message ="Bulunamadı"});
            }

            var mapper =_mapper.Map<GetCategoryByIdDto>(value);

            return Ok(mapper);
        }
    }
}