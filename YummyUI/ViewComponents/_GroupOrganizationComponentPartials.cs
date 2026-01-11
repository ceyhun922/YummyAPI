using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.GroupOrganizationChefDTO;

namespace YummyUI.ViewComponents
{
    public class _GroupOrganizationComponentPartials : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _GroupOrganizationComponentPartials(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Groups/groups/organizations/chefs");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultGroupOrganizationChefDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}