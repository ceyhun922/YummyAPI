using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.MessageDTOs;

namespace YummyUI.ViewComponents
{
    public class _AdminMessageComponentPartilas : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _AdminMessageComponentPartilas(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client =_httpClientFactory.CreateClient();
            var response =await client.GetAsync("http://localhost:5289/api/Contacts/ContactListIsReadFalse");

            if (response.IsSuccessStatusCode)
            {
                var jsonData =await response.Content.ReadAsStringAsync();
                var values =JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);

                ViewBag.messageCount =values?.Count();
                return View(values);
            }
            return View();
        }
    }

}