using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.DashboardDtos;

namespace YummyUI.ViewComponents
{
    public class _DashboardWidgetPartials : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardWidgetPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync("http://localhost:5289/apiDashboard/dashboard-widget");
            if (res.IsSuccessStatusCode)
            {
                var jsonData = await res.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<DashboardWidgetDto>(jsonData);
                return View(value);

            }
            return View();
        }
    }
}