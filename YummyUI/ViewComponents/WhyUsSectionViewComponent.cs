using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Newtonsoft.Json;
using YummyUI.DTOs.ServiceDTOs;
=======
>>>>>>> 4405c00 (UI Tema ViewComponentlere bölündü)

namespace YummyUI.ViewComponents
{
    public class WhyUsSectionViewComponent : ViewComponent
    {
<<<<<<< HEAD
        private readonly IHttpClientFactory _httpClientFactory;

        public WhyUsSectionViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client =_httpClientFactory.CreateClient();
            var response =await client.GetAsync("http://localhost:5289/api/Services/");

            if (response.IsSuccessStatusCode)
            {
                var jsonData =await response.Content.ReadAsStringAsync();
                var values =JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);

                return View(values);
            }
=======
        public IViewComponentResult Invoke()
        {
>>>>>>> 4405c00 (UI Tema ViewComponentlere bölündü)
            return View();
        }
    }
}