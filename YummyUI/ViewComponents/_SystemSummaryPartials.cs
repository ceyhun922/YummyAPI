using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.FeatureDTO;

namespace YummyUI.ViewComponents
{
    public class _SystemSummaryPartials : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _SystemSummaryPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client =_httpClientFactory.CreateClient();
            var response =await client.GetAsync("http://localhost:5289/apiDashboard/dashboard-summary");
            if (response.IsSuccessStatusCode)
            {
                var jsonData =await response.Content.ReadAsStringAsync();
                var value =JsonConvert.DeserializeObject<DashboardSummaryDto>(jsonData);
                return View(value);
            }
            return View();
        }
    }
}