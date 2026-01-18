using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.GalleryDTO;
using YummyUI.DTOs.OrganizationDTOs;

namespace YummyUI.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalleryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GalleryList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Galleries");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultGalleryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> DeleteGallery(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("http://localhost:5289/api/Galleries?id=" + id);
            return RedirectToAction("GalleryList");
        }

        public IActionResult CreateGallery()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateGallery(CreateGalleryDto createGalleryDto, IFormFile file)
        {
            var client = _httpClientFactory.CreateClient();

            if (file != null && file.Length > 0)
            {
                using var uploadContent = new MultipartFormDataContent();
                using var stream = file.OpenReadStream();

                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                uploadContent.Add(fileContent, "File", file.FileName);

                var fileResponse = await client.PostAsync("http://localhost:5289/api/FileImage", uploadContent);

                if (!fileResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Xeta");
                }
                var fileJson = await fileResponse.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(fileJson);
                var fileName = doc.RootElement.GetProperty("fileName").GetString();

                createGalleryDto.GalleryImageUrl = $"/images/{fileName}";

            }

            var jsonData = JsonConvert.SerializeObject(createGalleryDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5289/api/Galleries", content);
            if (!response.IsSuccessStatusCode)
            {
                return View(createGalleryDto);
            }

            return RedirectToAction("GalleryList");
        }

        public async Task<IActionResult> UpdateGallery(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5289/api/Galleries/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetByIdGalleryDto>(jsonData);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateGallery(GetByIdGalleryDto getByIdGalleryDto, IFormFile file)
        {
            var client = _httpClientFactory.CreateClient();

            if (file != null && file.Length > 0)
            {
              using var uploadContent =new MultipartFormDataContent();
              using var stream =file.OpenReadStream();

              var fileContent =new StreamContent(stream);
              fileContent.Headers.ContentType =new MediaTypeHeaderValue(file.ContentType);

              uploadContent.Add(fileContent,"File",file.FileName);

              var uploadResponse = await client.PostAsync("http://localhost:5289/api/FileImage",uploadContent);

              if (!uploadResponse.IsSuccessStatusCode)
              {
                ModelState.AddModelError("","Hata!");
                return View(getByIdGalleryDto);
              }

              var uploadJson =await uploadResponse.Content.ReadAsStringAsync();
              var doc =JsonDocument.Parse(uploadJson);
              var fileName =doc.RootElement.GetProperty("fileName").GetString();

              getByIdGalleryDto.GalleryImageUrl =$"/images/{fileName}";
            }

            var jsonData = JsonConvert.SerializeObject(getByIdGalleryDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:5289/api/Galleries", content);
            if (!response.IsSuccessStatusCode)
            {
                return View(getByIdGalleryDto);
            }
            return RedirectToAction("GalleryList");
        }
    }
}