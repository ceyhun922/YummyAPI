<<<<<<< HEAD
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.FooterDTOs;
=======
using Microsoft.AspNetCore.Mvc;
>>>>>>> 4405c00 (UI Tema ViewComponentlere bölündü)

namespace YummyUI.ViewComponents
{
    public class ContactSectionViewComponent : ViewComponent
    {
<<<<<<< HEAD
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactSectionViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:5289/api/Footers");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFooterDto>>(jsonData);
                return View(values);

            }
=======
         public IViewComponentResult Invoke()
        {
>>>>>>> 4405c00 (UI Tema ViewComponentlere bölündü)
            return View();
        }
    }
}