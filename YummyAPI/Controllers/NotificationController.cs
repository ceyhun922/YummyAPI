using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YummyAPI.Context;
using YummyAPI.DTOs.NotificationDTO;
using YummyAPI.Entities;

namespace YummyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public NotificationController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult NotificationList()
        {
            var notifications = _context.Notifications?.ToList();
            if (!notifications.Any())
            {
                return Ok();
            }
            var mapper = _mapper.Map<List<ResultNotificationDto>>(notifications);


            return Ok(mapper);
        }
        

        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var values = _mapper.Map<Notification>(createNotificationDto);
            _context.Notifications?.Add(values);
            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        public IActionResult DeleteNotification(int id)
        {
            var notification = _context.Notifications?.Find(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications?.Remove(notification);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            var mapper = _mapper.Map<Notification>(updateNotificationDto);
            _context.Notifications?.Update(mapper);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("IsReadFalse")]
        public IActionResult ResultNotificationIsReadFalse()
        {
            var value =_context.Notifications?.Where(x=>x.IsRead==false).ToList();
            var mapper =_mapper.Map<List<ResultNotificationReadFalseDto>>(value);
            return Ok(mapper);
        }
    }
}