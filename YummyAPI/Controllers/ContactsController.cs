using System.Threading.Tasks;
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
            var values = _context.Contacts?.Where(x => x.IsRead == true).ToList();
            var mapper = _mapper.Map<List<ResultContactDto>>(values);
            return Ok(mapper);
        }

        [HttpPost]
        public IActionResult ContactCreate(CreateContactDto createContactDto)
        {
            createContactDto.messageBox = MessageBoxType.Inbox;
            var mapper = _mapper.Map<Contact>(createContactDto);
            _context.Contacts?.Add(mapper);
            _context.SaveChanges();

            return Ok(mapper);
        }

        [HttpDelete]
        public IActionResult ContactDelete(int id)
        {
            var value = _context.Contacts?.Find(id);
            if (value == null) return Ok();

            _context.Contacts?.Remove(value);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult ContactUpdate(UpdateContactDto updateContactDto)
        {
            var mapper = _mapper.Map<Contact>(updateContactDto);
            if (mapper == null)
            {
                return Ok(mapper);
            }
            _context.Contacts?.Update(mapper);
            _context.SaveChanges();
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public IActionResult GetMessage(int id)
        {
            var value = _context.Contacts?.Find(id);
            var mapper = _mapper.Map<GetByIdContactDto>(value);
            return Ok(mapper);
        }


        [HttpPost("message/message-trash")]

        public async Task<IActionResult> MessageMoveTorash(int id)
        {
            var msj = _context.Contacts?.Find(id);
            if (msj == null)
            {
                return Ok(new { success = false, message = "Mesaj bulunmadı" });
            }
            msj.messageBox = MessageBoxType.Trash;
            msj.IsRead = false;
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Çöp kutusuna taşındı." });
        }

        [HttpPost("message/message-archive")]
        public async Task<IActionResult> MessageMoveToArchive(int id)
        {
            var msj = await _context.Contacts.FindAsync(id);
            if (msj == null)
            {
                return Ok(new { success = false, message = "Mesaj bulunmadı" });
            }
            msj.messageBox = MessageBoxType.Archived;
            msj.IsRead = false;
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Arşive taşındı" });
        }

        [HttpPost("message/message-restore")]
        public async Task<IActionResult> MessageRestore(int id)
        {
            var msj = await _context.Contacts.FindAsync(id);
            if (msj == null)
            {
                return Ok(new { success = false, message = "Mesaj bulunmadı" });
            }
            msj.messageBox =MessageBoxType.Inbox;
            msj.IsRead =true;
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Mesaj Geri Alındı" });
        }

        [HttpGet("message/message-achive-list")]
        public IActionResult MessageMoveToArchiveList()
        {
            var values = _context.Contacts?
                .Where(x => x.messageBox == MessageBoxType.Archived)
                .ToList();

            return Ok(values);
        }

        [HttpGet("message/message-trash-list")]
        public IActionResult MessageMoveToTrashList()
        {
            var values = _context.Contacts?
                .Where(x => x.messageBox == MessageBoxType.Trash)
                .ToList();

            return Ok(values);
        }
        [HttpGet("message/message-restore-list")]
        public IActionResult MessageMoveTorestoreList()
        {
            var values = _context.Contacts?
                .Where(x => x.messageBox == MessageBoxType.Inbox)
                .ToList();

            return Ok(values);
        }


    }
}