using System.Text;
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

            var form = new MultipartFormDataContent();
            form.Add(new StringContent(dto.ChefName ?? ""), "ChefName");
            form.Add(new StringContent(dto.ChefTitle ?? ""), "ChefTitle");
            form.Add(new StringContent(dto.ChefDescription ?? ""), "ChefDescription");
            form.Add(new StringContent(dto.ChefFacebookUrl ?? ""), "ChefFacebookUrl");
            form.Add(new StringContent(dto.ChefInstagramUrl ?? ""), "ChefInstagramUrl");
            form.Add(new StringContent(dto.ChefLinkedinUrl ?? ""), "ChefLinkedinUrl");
            form.Add(new StringContent(dto.ChefXUrl ?? ""), "ChefXUrl");
            form.Add(new StringContent(dto.ChefStatus.ToString()), "ChefStatus");

            if (file != null && file.Length > 0)
            {
                var stream = file.OpenReadStream();
                form.Add(new StreamContent(stream), "ChefImageUrl", file.FileName); 
            }

            var response = await client.PostAsync("http://localhost:5289/api/Chefs", form);

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