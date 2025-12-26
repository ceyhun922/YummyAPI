using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.ProductDTOs;

namespace YummyUI.ViewComponents
{
    public class _MenuBreakfastSectionPartials : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _MenuBreakfastSectionPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client =_httpClientFactory.CreateClient();
            var response =await client.GetAsync("http://localhost:5289/api/Products");

            if (response.IsSuccessStatusCode)
            {
                var jsonData =await response.Content.ReadAsStringAsync();
                var values =JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}