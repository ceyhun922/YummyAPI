using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YummyUI.Models.Setting;

namespace YummyUI.Controllers
{
    public class AIController : Controller
    {
        private readonly string _apiKey;
        private readonly IHttpClientFactory _httpClientFactory;

        public AIController(IOptions<OpenAISetting> openAI, IHttpClientFactory httpClientFactory)
        {
            _apiKey = openAI.Value.ApiKey ?? "";
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult CreateRecipe() => View();

        [HttpPost]

        public async Task<IActionResult> CreateRecipe(string promt)
        {
            if (string.IsNullOrWhiteSpace(promt))
            {
                ViewBag.recipe = "Prompt boş ola bilməz.";
                return View();
            }

            var client = _httpClientFactory.CreateClient();

            // 401 görürsen, burası BOŞ demekdir:
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                ViewBag.recipe = "ApiKey boş gəlir. appsettings section/binding yoxla.";
                return View();
            }

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            var requestData = new
            {
                model = "gpt-4o-mini",
                messages = new object[]
                {
                    new { role = "system", content = "Sen bir restoran için yemek önerileri yapan yapay zeka aracısın. Kullanıcının malzemelerine uygun yemek tarifleri üret." },
                    new { role = "user", content = promt }
                },
                temperature = 0.7
            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.recipe = $"Hata: {(int)response.StatusCode} {response.StatusCode}\n{body}";
                return View();
            }

            var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
            ViewBag.recipe = result?.choices?.FirstOrDefault()?.message?.content ?? "(Boş cevap döndü)";

            return View();
        }

        [HttpPost("ai/reply-email")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReplyEmail([FromForm] string promt)
        {
            if (string.IsNullOrWhiteSpace(promt))
                return BadRequest(new { ok = false, message = "Prompt boş olamaz." });

            if (string.IsNullOrWhiteSpace(_apiKey))
                return StatusCode(500, new { ok = false, message = "ApiKey boş" });

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            var requestData = new
            {
                model = "gpt-4o-mini",
                messages = new object[]
                {
                    new { role = "system", content = "Mesajları okuyarak ilgili mesaja ilgili cevap veren gelişmiş Yapay Zekasın" },
                    new { role = "user", content = promt }
                },
                temperature = 0.7
            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, new { ok = false, message = body });

            var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
            var reply = result?.choices?.FirstOrDefault()?.message?.content ?? "(Boş cevap döndü)";

            return Ok(new { ok = true, reply });
        }

        public class OpenAIResponse
        {
            public List<Choice> choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }
    }
}
