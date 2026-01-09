using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.MessageDTOs;

namespace YummyUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MessageList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Contacts");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> MessageDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5289/api/Contacts/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetByIdMessageDto>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MessageTrash(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync($"http://localhost:5289/api/Contacts/message/MessageTrash?id={id}", null);

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { success = false, message = "Taşınamadı" });

            return Ok(new { success = true, message = "Çöp kutusuna taşındı." });
        }

        public async Task<IActionResult> MessageTrashList()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("http://localhost:5289/api/Contacts/message/MessageTrashList");
            if (!res.IsSuccessStatusCode)
                return Json(new List<ResultMessageDto>());

            var json = await res.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ResultMessageDto>>(json);
            return Json(data);

        }




    }
}