using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.OrganizationDTOs;

namespace YummyUI.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrganizationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OrganizationList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Organizations");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOrganizationDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public IActionResult CreateOrganization()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrganization(CreateOrganizationDto createOrganizationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createOrganizationDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5289/api/Organizations", content);

            if (!response.IsSuccessStatusCode)
            {
                return View(createOrganizationDto);

            }
            return RedirectToAction("OrganizationList");
        }

        public async Task<IActionResult> UpdateOrganization(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:5289/api/Organizations/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<GetByIdOrganizationDto>(jsonData);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrganization(GetByIdOrganizationDto getByIdOrganizationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData =JsonConvert.SerializeObject(getByIdOrganizationDto);
            var content = new StringContent(jsonData,Encoding.UTF8,"application/json");
            var response =await client.PutAsync("http://localhost:5289/api/Organizations",content);
            if (!response.IsSuccessStatusCode)
            {
                return View(getByIdOrganizationDto);
            }
            return RedirectToAction("OrganizationList");
        }
    }
}
//http://localhost:5289/api/Organizations/2
