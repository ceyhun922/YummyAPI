using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyAPI.DTOs.AboutDTO;
using YummyUI.DTOs.AboutDTO;

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
        public async Task<IActionResult> CreateAbout(CreateAboutDto creatAboutDto)
        {
            var client =_httpClientFactory.CreateClient();
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