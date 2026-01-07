using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.RezervationDTOs;

namespace YummyUI.Controllers
{
    public class RezervationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RezervationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> RezervationList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Rezervations");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultRezervationDto>>(jsonData);
                return View(values);

            }
            return View();
        }

        public IActionResult CreateRezervation() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRezervation(CreateRezervationDto createRezervationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createRezervationDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5289/api/Rezervations", content);
            if (!response.IsSuccessStatusCode)
            {
                return View(createRezervationDto);

            }
            return RedirectToAction("RezervationList");
        }

        public async Task<IActionResult> DeleteRezervation(int id)
        {
            var client =_httpClientFactory.CreateClient();
            await client.DeleteAsync("http://localhost:5289/api/Rezervations?id=" +id);
            return RedirectToAction("RezervationList");
        }

        public async Task<IActionResult> UpdateRezervation(int id)
        {
            var client =_httpClientFactory.CreateClient();
            var response =await client.GetAsync($"http://localhost:5289/api/Rezervations/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData =await response.Content.ReadAsStringAsync();
                var value =JsonConvert.DeserializeObject<GetByIdRezervationDto>(jsonData);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRezervation(GetByIdRezervationDto getByIdRezervationDto)
        {
            var client =_httpClientFactory.CreateClient();
            var jsonData =JsonConvert.SerializeObject(getByIdRezervationDto);
            StringContent stringContent =new(jsonData,Encoding.UTF8,"application/json");
            var response =await client.PutAsync("http://localhost:5289/api/Rezervations",stringContent);
            if (!response.IsSuccessStatusCode)
            {
                return View(getByIdRezervationDto);
            }
            return RedirectToAction("RezervationList");
        }
    }
}