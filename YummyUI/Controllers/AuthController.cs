using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyAPI.DTOs.AboutDTO;
using YummyUI.DTOs.AuthDTOs;

namespace YummyUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(ResultAboutDto resultAboutDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(resultAboutDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5289/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                return View(resultAboutDto);
            }

            var body = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonConvert.DeserializeObject<LoginResponseDto>(body);

            if (tokenObj == null || string.IsNullOrWhiteSpace(tokenObj.Token))
            {
                ModelState.AddModelError("", "Token alınmadı");
                return View(resultAboutDto);
            }

            // ✅ tokeni cookie-də saxla
            Response.Cookies.Append("jwt", tokenObj.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // prod-da true
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });


            return RedirectToAction("Index", "Admin");
        }
    }
}