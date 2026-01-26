<<<<<<< HEAD
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YummyUI.DTOs.RezervationDTOs;
=======
using Microsoft.AspNetCore.Mvc;
>>>>>>> 4405c00 (UI Tema ViewComponentlere bölündü)

namespace YummyUI.ViewComponents
{
    public class BookTableSectionViewComponent : ViewComponent
    {
<<<<<<< HEAD
        private readonly IHttpClientFactory _httpClientFactory;

        public BookTableSectionViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

=======
>>>>>>> 4405c00 (UI Tema ViewComponentlere bölündü)
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}