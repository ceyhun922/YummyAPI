using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.ContactDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ContactsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ContactList()
        {
            var values = _context.Contacts?.ToList();
            var mapper = _mapper.Map<List<ResultContactDto>>(values);

            return Ok(new { message = "Contact List", data = mapper });
        }

        [HttpPost]
        public IActionResult ContactCreate(CreateContactDto createContactDto)
        {
            var mapper = _mapper.Map<Contact>(createContactDto);

            _context.Contacts?.Add(mapper);
            _context.SaveChanges();

            return Ok(new { message = "Eklendi" });
        }

        [HttpDelete]
        public IActionResult ContactDelete(int id)
        {
            var value = _context.Contacts?.Find(id);
            if (value == null) return Ok(new { message = "Tapılmadı" });

            _context.Contacts?.Remove(value);
            _context.SaveChanges();
            return Ok(new { message = "Silindi" });
        }

        [HttpPut]
        public IActionResult ContactUpdate(UpdateContactDto updateContactDto)
        {
            var mapper = _mapper.Map<Contact>(updateContactDto);
            if (mapper == null)
            {
                return Ok(new { message = "Tapılmadı" });
            }
            _context.Contacts?.Update(mapper);
            _context.SaveChanges();
            return Ok(new { message = "Yenilendi" });
        }
    }
}