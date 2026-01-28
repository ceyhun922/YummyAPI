using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.RezervationDTOs;
using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class BookTableSectionViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookTableSectionViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}