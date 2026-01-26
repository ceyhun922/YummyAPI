using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Newtonsoft.Json;
using YummyUI.DTOs.CategoryDTOs;
=======
>>>>>>> 4405c00 (UI Tema ViewComponentlere bölündü)

namespace YummyUI.ViewComponents
{
    public class MenuSectionViewComponent : ViewComponent
    {
<<<<<<< HEAD
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
=======
        public IViewComponentResult Invoke()
        {
>>>>>>> 4405c00 (UI Tema ViewComponentlere bölündü)
            return View();
        }
    }
}