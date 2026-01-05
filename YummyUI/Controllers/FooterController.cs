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
            var client =_httpClientFactory.CreateClient();
            var response =await client.GetAsync("http://localhost:5289/api/Footers");
            if (response.IsSuccessStatusCode)
            {
                var jsonData =await response.Content.ReadAsStringAsync();
                var values =JsonConvert.DeserializeObject<List<ResultFooterDto>>(jsonData);
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
        public IActionResult CreateFooterArea(CreateFooterDto createFooterDto)
        {
            return View();
        }
        public IActionResult DeleteFooterArea()
        {
            return View();
        }
        public IActionResult UpdateFooterArea(int id)
        {
            return View();
        }
        public IActionResult UpdateFooterArea(GetByIdFooterDto getByIdFooterDto)
        {
            return View();
        }
    }
}