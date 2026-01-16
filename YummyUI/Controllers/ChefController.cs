using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.ChefDTOs;

namespace YummyUI.Controllers
{
    public class ChefController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChefController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChef(CreateChefDto dto, IFormFile file)
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
                    return View(dto);
                }

                var uploadJson = await uploadResp.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(uploadJson);
                var fileName = doc.RootElement.GetProperty("fileName").GetString();

                dto.ImageFile = $"/images/{fileName}";
            }

            var json = System.Text.Json.JsonSerializer.Serialize(dto);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5289/api/Chefs", content);

            if (!response.IsSuccessStatusCode)
                return View(dto);

            return RedirectToAction("ChefList");
        }



        public async Task<IActionResult> ChefList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Chefs");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultChefDto>>(jsonData);
                return View(values);
            }
            return View();

        }
        public IActionResult CreateChef()
        {
            return View();
        }


        public async Task<IActionResult> DeleteChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("http://localhost:5289/api/Chefs?id=" + id);

            return RedirectToAction("ChefList");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Chefs/GetOneChef?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdChefDto>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> UpdateChef(GetByIdChefDto getByIdChefDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(getByIdChefDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:5289/api/Chefs", content);
            if (!response.IsSuccessStatusCode)
            {
                return View(getByIdChefDto);

            }
            return RedirectToAction("ChefList");


        }

    }
}