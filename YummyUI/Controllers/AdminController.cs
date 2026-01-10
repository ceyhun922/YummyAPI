using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.CategoryDTOs;
using YummyUI.DTOs.DashboardDtos;
using YummyUI.DTOs.MessageDTOs;

namespace YummyUI.Controllers
{

    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

                [HttpGet]
        public async Task<IActionResult> Revenue()
        {
            var client = _httpClientFactory.CreateClient();

            var url = $"http://localhost:5289/apiDashboard/revenue";

            var res = await client.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
                return StatusCode((int)res.StatusCode, json);

            var dto = JsonConvert.DeserializeObject<DashboardRevenueDto>(json);
            return Json(dto);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MessagesList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Contacts");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(values);
            }
            return View();
        }


        
       
        public IActionResult ProductList()
        {
            return View();
        }
        public IActionResult ChefList()
        {
            return View();
        }
    }
}