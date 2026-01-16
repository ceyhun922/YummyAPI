using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.FeatureDTO;
using YummyUI.DTOs.ProductDTOs;

namespace YummyUI.Controllers
{
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> FeatureArea()
        {
            var client = _httpClientFactory.CreateClient();
            
            var response = await client.GetAsync("http://localhost:5289/api/Features");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        
        [HttpGet]
        public IActionResult CreateFeature()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto, IFormFile file)
        {
            var client = _httpClientFactory.CreateClient();
            if (file != null && file.Length > 0)
            {
                using var uploadContent =new MultipartFormDataContent();
                using var stream =file.OpenReadStream();

                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                uploadContent.Add(fileContent,"File",file.FileName);

                var uploadResponse =await client.PostAsync("http://localhost:5289/api/FileImage",uploadContent);
                if (!uploadResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("","Öne Çıkan Alan Resmi Yüklenmedi");
                    return View(createFeatureDto);
                }

                var uploadJson = await uploadResponse.Content.ReadAsStringAsync();
                var doc =JsonDocument.Parse(uploadJson);
                var fileName =doc.RootElement.GetProperty("fileName").GetString();

                createFeatureDto.FeatureImageUrl =$"/images/{fileName}";
            }
            var jsonData = JsonConvert.SerializeObject(createFeatureDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5289/api/Features", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("FeatureArea");
            }
            return View();
        }

        public async Task<IActionResult> FeatureDelete(int id)
        {
            var client =_httpClientFactory.CreateClient();
            await client.DeleteAsync("http://localhost:5289/api/Features?id="+id);
            return RedirectToAction("FeatureArea");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeature(int id)  
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5289/api/Features/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdFeatureDto>(jsonData);
                return View(values);
            }
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> UpdateFeature(GetByIdFeatureDto getByIdFeatureDto)
        {
            var client =_httpClientFactory.CreateClient();
            var jsonData =JsonConvert.SerializeObject(getByIdFeatureDto);
            var content =new StringContent(jsonData,Encoding.UTF8,"application/json");
            
            var response =await client.PutAsync("http://localhost:5289/api/Features",content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("FeatureArea");
            }
            return View();
        }

      
    }
}