using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.FeatureDTO;
using YummyUI.DTOs.FooterDTOs;

namespace YummyUI.Controllers
{
    public class FooterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FooterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> FooterArea()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Footers");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFooterDto>>(jsonData);
                values?.FirstOrDefault();
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateFooterArea()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFooterArea(CreateFooterDto createFooterDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFooterDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("", content);

            if (!response.IsSuccessStatusCode)
            {
                return View(createFooterDto);
            }
            return RedirectToAction("FooterArea");
        }
        public async Task<IActionResult> DeleteFooterArea(int id)
        {
            var client =_httpClientFactory.CreateClient();
            await client.DeleteAsync("http://localhost:5289/api/Footers?id" + id);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateFooterArea(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5289/api/Footers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdFooterDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFooterArea(GetByIdFooterDto getByIdFooterDto)
        {
             var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(getByIdFooterDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:5289/api/Footers", content);

            if (!response.IsSuccessStatusCode)
            {
                return View(getByIdFooterDto);
            }
            return RedirectToAction("FooterArea");
        }
    }
}