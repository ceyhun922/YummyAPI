using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using YummyAPI.DTOs.AboutDTO;
using YummyUI.DTOs.AboutDTO;
using System.Text.Json;

namespace YummyUI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> AboutArea()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/About");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto creatAboutDto, IFormFile file)
        {
            var client =_httpClientFactory.CreateClient();
            if (file != null && file.Length > 0)
            {
                using var uploadContent =new MultipartFormDataContent();
                using var stream =file.OpenReadStream();
                
                var fileContent =new StreamContent(stream);
                fileContent.Headers.ContentType =new MediaTypeHeaderValue(file.ContentType);

                uploadContent.Add(fileContent, "File", file.FileName);

                var uploadResponse =await client.PostAsync("http://localhost:5289/api/FileImage",uploadContent);

                if (!uploadResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("","Hakkında Alanına File Yüklenirken Hata oluştu");
                    return View(creatAboutDto);
                }
                var uploadJson =await uploadResponse.Content.ReadAsStringAsync();
                var doc =JsonDocument.Parse(uploadJson);
                var fileName =doc.RootElement.GetProperty("fileName").GetString();
                
                creatAboutDto.AboutImageUrl =$"/images/{fileName}";
            }
            var jsonData =JsonConvert.SerializeObject(creatAboutDto);
            var content =new StringContent(jsonData,Encoding.UTF8,"application/json");
            var response =await client.PostAsync("http://localhost:5289/api/About",content);
            if (!response.IsSuccessStatusCode)
            {
                return View(creatAboutDto);
            }
            return RedirectToAction("AboutArea");
            
        }

        [HttpGet]
        public async Task<IActionResult> AboutUpdate(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/About/GetByIdAboutArea?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetAboutByIdDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AboutUpdate(GetAboutByIdDto getAboutByIdDto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(getAboutByIdDto);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("http://localhost:5289/api/About", content);
            if (!response.IsSuccessStatusCode)
            {
                return View(getAboutByIdDto);
            }
            return RedirectToAction("AboutArea");

        }

        public async Task<IActionResult> AboutDelete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("http://localhost:5289/api/About?id=" + id);

            return RedirectToAction("AboutArea");
        }
    }
}