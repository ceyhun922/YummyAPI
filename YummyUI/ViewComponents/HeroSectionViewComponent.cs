using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Newtonsoft.Json;
using YummyUI.DTOs.FeatureDTO;
=======
>>>>>>> 4405c00 (UI Tema ViewComponentlere bölündü)

namespace YummyUI.ViewComponents
{
    public class HeroSectionViewComponent : ViewComponent
    {
<<<<<<< HEAD
        private readonly IHttpClientFactory _httpClientFactory;

        public HeroSectionViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Features");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);

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