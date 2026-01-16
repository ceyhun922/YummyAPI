using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.TestimonialDTOs;

namespace YummyUI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> TestimonialList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Testimonials");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto, IFormFile file)
        {
            var client = _httpClientFactory.CreateClient();

            if (file != null && file.Length > 0)
            {
                using var uploadContent = new MultipartFormDataContent();
                using var stream = file.OpenReadStream();

                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                uploadContent.Add(fileContent, "File", file.FileName);

                var uploadResp = await client.PostAsync("http://localhost:5289/api/FileImage", uploadContent);
                if (!uploadResp.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Şəkil yükləmə alınmadı.");
                    return View(createTestimonialDto);
                }

                var uploadJson = await uploadResp.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(uploadJson);
                var fileName = doc.RootElement.GetProperty("fileName").GetString();

                createTestimonialDto.TestimonialImageUrl = $"/images/{fileName}";
            }
            var jsonData = JsonConvert.SerializeObject(createTestimonialDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5289/api/Testimonials", content);
            if (!response.IsSuccessStatusCode)
            {
                return View(createTestimonialDto);
            }
            return RedirectToAction("TestimonialList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5289/api/Testimonials/{id}");


            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdTestimonialDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(GetByIdTestimonialDto getByIdTestimonialDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(getByIdTestimonialDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:5289/api/Testimonials", content);

            if (!response.IsSuccessStatusCode)
            {
                return View(getByIdTestimonialDto);

            }
            return RedirectToAction("TestimonialList");
        }
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"http://localhost:5289/api/Testimonials?id={id}");
            return RedirectToAction("TestimonialList");
        }
    }
}