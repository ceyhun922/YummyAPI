using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.CategoryDTOs;

namespace YummyUI.ViewComponents
{
    public class MenuSectionViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MenuSectionViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client =_httpClientFactory.CreateClient();
            var response =await client.GetAsync("http://localhost:5289/api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonData =await response.Content.ReadAsStringAsync();
                var values =JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}