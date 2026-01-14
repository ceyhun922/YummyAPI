


using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace YummyUI.Controllers
{
    public class FileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> UploadFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["warning"] = "Dosya Seçilmedi";
                return RedirectToAction(nameof(UploadFile));
            }
            var client = _httpClientFactory.CreateClient();
            using var content = new MultipartFormDataContent();
            using var stream = file.OpenReadStream();

            var fileContent = new StreamContent(stream);

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent, "File", file.FileName);

            var response = await client.PostAsync("http://localhost:5289/api/FileImage", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = "API Upload hatası";
                return RedirectToAction(nameof(UploadFile));
            }

            var json = await response.Content.ReadAsStringAsync();

            var doc = System.Text.Json.JsonDocument.Parse(json);

            var fileName = doc.RootElement.GetProperty("fileName").GetString();


            ViewBag.ImageUrl = $"http://localhost:5289/images/{fileName}";

            return View();
        }
    }
}