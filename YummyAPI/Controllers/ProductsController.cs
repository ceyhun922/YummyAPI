using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YummyAPI.Context;
using YummyAPI.DTOs.ProductDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var values = _mapper.Map<Product>(createProductDto);
            _context.Products?.Add(values);
            _context.SaveChanges();
            return Ok(new { message = "Eklendi" });
        }
        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _context.Products?.ToList();
            var mapper = _mapper.Map<List<ResultProductDto>>(values);
            return Ok(mapper);
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var mapper = _mapper.Map<Product>(updateProductDto);
            _context.Products?.Update(mapper);
            _context.SaveChanges();
            return Ok(new { message = "Yenilendi" });
        }

        [HttpGet("GetOneProduct")]
        public IActionResult GetOneProduct(int id)
        {
            var value = _context.Products?.Find(id);

            var mapper = _mapper.Map<GetByIdProductDto>(value);

            if (mapper == null)
            {
                return Ok(new { message = "Tapılmadı" });
            }

            return Ok(new { message = "Tapıldı", data = mapper });
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var value = _context.Products?.Find(id);

            if (value == null)
            {
                return Ok(new { message = "Tapılmadı" });
            }
            _context.Products?.Remove(value);
            _context.SaveChanges();
            return Ok(new { message = "Silindi" });
        }
        [HttpGet("GetAllProductWithCategory")]
        public IActionResult GetAllProductWithCategory()
        {
            var values =_context.Products?.Where(x=>x.ProductStatus ==true).Include(x=>x.Category).ToList();
            var mapper =_mapper.Map<List<ResultGetAllProductWithCategoryDto>>(values);
            return Ok(mapper);
        }
        
    }
}