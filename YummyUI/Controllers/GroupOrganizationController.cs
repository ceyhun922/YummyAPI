using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.GroupOrganizationChefDTO;

namespace YummyUI.Controllers
{
    public class GroupOrganizationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GroupOrganizationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GroupOrganizationList()
        {
            var client =_httpClientFactory.CreateClient();


            var response =await client.GetAsync("http://localhost:5289/api/Groups/groups/organizations/chefs");
            if (response.IsSuccessStatusCode)
            {
                var jsonData =await response.Content.ReadAsStringAsync();
                var values =JsonConvert.DeserializeObject<List<ResultGroupOrganizationChefDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}