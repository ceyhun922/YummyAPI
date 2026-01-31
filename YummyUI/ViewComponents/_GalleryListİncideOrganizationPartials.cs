using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.GalleryDTO;

namespace YummyUI.ViewComponents
{
    public class _GalleryListİncideOrganizationPartials : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _GalleryListİncideOrganizationPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client =_httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Galleries");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultGalleryDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}