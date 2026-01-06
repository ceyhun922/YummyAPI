using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.FeatureDTO;
using YummyUI.DTOs.FooterDTOs;

namespace YummyUI.ViewComponents
{
    public class _LayoutFooterSectionPartials : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _LayoutFooterSectionPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Footers/FooterBottomArea");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultFooterBottomDto>(jsonData);
                return View(values);

            }
            return View();
        }
    }
}