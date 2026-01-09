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

        public async Task<IActionResult> MessageList(string box = "inbox")
        {
            box = (box ?? "inbox").ToLower().Trim();
            ViewBag.ActiveBox = box;

            var client = _httpClientFactory.CreateClient();

            // 1) hansı list göstəriləcək?
            string listUrl = box switch
            {
                "trash" => "http://localhost:5289/api/Contacts/message/message-trash-list",
                _ => "http://localhost:5289/api/Contacts"
            };

            // 2) list datanı çək
            var values = new List<ResultMessageDto>();
            var res = await client.GetAsync(listUrl);
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(json) ?? new();
            }

            // 3) count-ları doldur (ayrı call lazımdır)
            // inbox count
            var inboxCount = 0;
            var inboxRes = await client.GetAsync("http://localhost:5289/api/Contacts");
            if (inboxRes.IsSuccessStatusCode)
            {
                var j = await inboxRes.Content.ReadAsStringAsync();
                inboxCount = (JsonConvert.DeserializeObject<List<ResultMessageDto>>(j) ?? new()).Count;
            }

            // trash count
            var trashCount = 0;
            var trashRes = await client.GetAsync("http://localhost:5289/api/Contacts/message/message-trash-list");
            if (trashRes.IsSuccessStatusCode)
            {
                var j = await trashRes.Content.ReadAsStringAsync();
                trashCount = (JsonConvert.DeserializeObject<List<ResultMessageDto>>(j) ?? new()).Count;
            }

            ViewBag.CntInbox = inboxCount;
            ViewBag.CntTrash = trashCount;
            ViewBag.CntArchive = 0; // archive endpointin yoxdur hələ

            return View(values); // MessageList.cshtml
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
            var response = await client.PostAsync($"http://localhost:5289/api/Contacts/message/message-trash?id={id}", null);

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { success = false, message = "Taşınamadı" });

            TempData["ToastMessage"] = "Çöpe taşındı.";
            return RedirectToAction("MessageList", new { box = "inbox" });
        }

        public async Task<IActionResult> MessageTrashList()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("http://localhost:5289/api/Contacts/message/message-trash-list");
            if (!res.IsSuccessStatusCode)
                return Json(new List<ResultMessageDto>());

            var json = await res.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ResultMessageDto>>(json);
            return Json(data);
        }

        public async Task<IActionResult> MessageDelete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.DeleteAsync("http://localhost:5289/api/Contacts?id=" + id);

            if (!res.IsSuccessStatusCode)
                return BadRequest(new { success = false, message = "Silinemedi" });

            return RedirectToAction("MessageList");
        }




    }
}