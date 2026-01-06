using Microsoft.AspNetCore.Mvc;

namespace YummyUI.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult GalleryList()
        {
            return View();
        }
    }
}