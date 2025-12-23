using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using YummyUI.DTOs.ChefDTOs;
using YummyUI.DTOs.GroupOrganizationChefDTO;
using YummyUI.DTOs.GroupOrganizationDTO;
using YummyUI.DTOs.OrganizationDTOs;

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
        private async Task FillDropdowns()
        {
            var client = _httpClientFactory.CreateClient();

            var chefsResponse = await client.GetAsync("http://localhost:5289/api/Chefs");
            var chefsJson = await chefsResponse.Content.ReadAsStringAsync();
            var chefs = JsonConvert.DeserializeObject<List<ResultChefDto>>(chefsJson) ?? new();
            ViewBag.Chefs = new SelectList(chefs, "ChefId", "ChefName");

            var orgResponse = await client.GetAsync("http://localhost:5289/api/Organizations");
            var orgJson = await orgResponse.Content.ReadAsStringAsync();
            var organizations = JsonConvert.DeserializeObject<List<ResultOrganizationDto>>(orgJson) ?? new();
            ViewBag.Organizations = new SelectList(organizations, "OrganizationId", "OrganizationName");
        }

        [HttpGet]
        public async Task<IActionResult> CreateGroupOrganization()
        {
            await FillDropdowns();
            return View(new CreateGroupOrganizationDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGroupOrganization(CreateGroupOrganizationDto dto)
        {
            if (!ModelState.IsValid)
            {
                await FillDropdowns();
                return View(dto);
            }

            var client = _httpClientFactory.CreateClient();

            var payload = new
            {
                dto.OrganizationId,
                dto.GroupPriority,
                dto.PersonCount,
                dto.ParticipantCount,
                Date = dto.Date.ToString("yyyy-MM-dd"),
                Time = dto.Time.ToString("HH:mm:ss"),
                dto.ChefIds
            };

            var jsonData = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5289/api/GroupOrganizations", content);

            if (!response.IsSuccessStatusCode)
            {
                var apiError = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"API Error ({(int)response.StatusCode}): {apiError}");

                await FillDropdowns();
                return View(dto);
            }

            return RedirectToAction("GroupOrganizationList");
        }

        


    }
}