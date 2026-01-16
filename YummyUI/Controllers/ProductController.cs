using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using YummyUI.DTOs.CategoryDTOs;
using YummyUI.DTOs.ProductDTOs;

namespace YummyUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ProductList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5289/api/Products/GetAllProductWithCategory");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Categories");
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> categoryValues = (from x in values
                                                   select (new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   })).ToList();
            ViewBag.CategoryList = categoryValues;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto, IFormFile file)
        {
            var client = _httpClientFactory.CreateClient();

            if (file != null && file.Length > 0)
            {
                using var uploadContent = new MultipartFormDataContent();
                using var stream = file.OpenReadStream();

                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                uploadContent.Add(fileContent, "File", file.FileName);

                var response = await client.PostAsync("http://localhost:5289/api/FileImage", uploadContent);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Hata");
                    return View(createProductDto);
                }
                var uploadJson = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(uploadJson);
                var fileName = doc.RootElement.GetProperty("fileName").GetString();

                createProductDto.ImageFile = $"/images/{fileName}";
            }
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5289/api/Products", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            return View();
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("http://localhost:5289/api/Products?id=" + id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var ProductResponse = await client.GetAsync("http://localhost:5289/api/Products/GetOneProduct?id=" + id);
            var productJson = await ProductResponse.Content.ReadAsStringAsync();
            var Product = JsonConvert.DeserializeObject<GetByIdProductDto>(productJson);

            var CategoryResponse = await client.GetAsync("http://localhost:5289/api/Categories");
            var CategoryJson = await CategoryResponse.Content.ReadAsStringAsync();
            var Categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(CategoryJson);

            ViewBag.CategoryList = (from c in Categories
                                    select (new SelectListItem
                                    {
                                        Text = c.CategoryName,
                                        Value = c.CategoryId.ToString()
                                    })).ToList();

            return View(Product);
        }

        public async Task<IActionResult> ProductUpdate(GetByIdProductDto getByIdProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(getByIdProductDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("http://localhost:5289/api/Products", content);

            if (!response.IsSuccessStatusCode)
                return View(getByIdProductDto);

            return RedirectToAction("ProductList");
        }
    }
}