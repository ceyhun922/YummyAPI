using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.CategoryDTOs;

namespace YummyUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private class ToggleStatusResponse
        {
            public bool success { get; set; }
            public bool status { get; set; }
            public string message { get; set; }
        }

        public async Task<IActionResult> CategoryList()
        {
            var list = _httpClientFactory.CreateClient();
            var response = await list.GetAsync("http://localhost:5289/api/Categories");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5289/api/Categories", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }

            return View();
        }

        public async Task<IActionResult> CategoryDelete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("http://localhost:5289/api/Categories?id=" + id);

            return RedirectToAction("CategoryList");
        }



        public async Task<IActionResult> CategoryUpdate(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Categories/" + id);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetCategoryByIdDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeCategoryStatus(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(
                $"http://localhost:5289/api/Categories/toggle-status/{id}",
                null
            );

            if (!response.IsSuccessStatusCode)
                return Json(new { success = false, message = "API isteği başarısız." });

            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }








    }
}