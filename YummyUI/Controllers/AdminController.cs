using Microsoft.AspNetCore.Mvc;

namespace YummyUI.Controllers
{   

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}