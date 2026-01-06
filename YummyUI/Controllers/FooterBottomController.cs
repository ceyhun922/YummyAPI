using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.FooterDTOs;

namespace YummyUI.Controllers
{
    public class FooterBottomController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FooterBottomController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> FooterBottomArea()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Footers");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFooterBottomDto>>(jsonData);
                values?.FirstOrDefault();
                return View(values);
            }
            return View();
        }
    }
}