using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
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

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                ViewBag.recipe = "ApiKey boş gəlir. appsettings section/binding yoxla.";
                return View();
            }

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            var requestData = new
            {
                model = "gpt-4.1-mini",

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

        [HttpPost("reply-email")]
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
                model = "gpt-4.1-mini",

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


        [HttpGet]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public async Task<IActionResult> WorldFoods([FromQuery] int count = 4)
{
    if (string.IsNullOrWhiteSpace(_apiKey))
        return StatusCode(500, new { ok = false, message = "ApiKey boş" });

    if (count < 1) count = 1;
    if (count > 8) count = 8;

    var client = _httpClientFactory.CreateClient();
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", _apiKey);

    var requestData = new
    {
        model = "gpt-4.1-mini",
        response_format = new { type = "json_object" }, // ✅ JSON dışına çıkmayı çok azaltır
        messages = new object[]
        {
            new {
                role = "system",
                content =
$@"Sadece ve sadece GEÇERLİ JSON döndür. JSON DIŞINDA TEK KARAKTER YAZMA.

Şema:
{{
  ""items"": [
    {{
      ""country"": ""İtalya"",
      ""countryCode"": ""it"",
      ""dish"": ""Risotto alla Milanese"",
      ""description"": ""kısa"",
      ""durationMin"": 35,
      ""difficulty"": ""Kolay|Orta|Zor"",
      ""imageQuery"": ""risotto"",
      ""tags"": [""pirinç"", ""safran""]
    }}
  ]
}}

Kurallar:
- items uzunluğu: {count}
- Ülkeler farklı, yemekler farklı
- countryCode alpha-2 küçük harf
- durationMin 15-60
- difficulty sadece Kolay/Orta/Zor
- description tek cümle, max 120 karakter
- tags 2-6 adet"
            },
            new {
                role = "user",
                content = $"Bugün için {count} farklı ülke ve {count} farklı yemek öner. Seed:{DateTime.UtcNow:O}"
            }
        },
        temperature = 1.0
    };

    var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);
    var body = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
        return StatusCode((int)response.StatusCode, new { ok = false, message = body });

    var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
    var text = result?.choices?.FirstOrDefault()?.message?.content?.Trim() ?? "";

    try
    {
        using var doc = JsonDocument.Parse(text);
        // ✅ doc dispose olunca patlamasın diye clone şart
        var root = doc.RootElement.Clone();

        Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "0";

        return new JsonResult(new { ok = true, data = root });
    }
    catch
    {
        return new JsonResult(new { ok = true, parseError = true, dataRaw = text });
    }
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
