

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.ChefDTOs;



namespace YummyUI.ViewComponents
{
    public class ChefSectionViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChefSectionViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Chefs/");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultChefDto>>(jsonData);

                return View(values);
            }
            return View();
        }
    }
}

