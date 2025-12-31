

using System.Net.Http.Headers;
using System.Net.Http.Json; // <-- BUNU EKLE
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YummyUI.Models.Setting;

namespace YummyUI.Controllers
{
    public class AIController : Controller
    {
        private readonly string _apiKey;

        public AIController(IOptions<ApiSetting> apiSetting)
        {
           _apiKey = apiSetting.Value.ApiKey;
        }

        [HttpGet]
        public IActionResult CreateRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(string promt)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            var requestData = new
            {
                model = "gpt-4o-mini",
                messages = new object[]
                {
                    new
                    {
                        role = "system",
                        content = "Sen bir restoran için yemek önerileri yapan yapay zeka aracısın. Kullanıcının malzemelerine uygun yemek tarifleri üret."
                    },
                    new
                    {
                        role = "user",
                        content = promt
                    }
                },
                temperature = 0.7
            };

            var response = await client.PostAsJsonAsync(
                "https://api.openai.com/v1/chat/completions",
                requestData
            );

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
                ViewBag.recipe = result?.choices?.FirstOrDefault()?.message?.content ?? "(Boş cevap döndü)";
            }
            else
            {
                ViewBag.recipe = $"Hata: {(int)response.StatusCode} {response.StatusCode}\n{body}";
            }

            return View();
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
