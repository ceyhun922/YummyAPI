using Microsoft.AspNetCore.Mvc;

namespace YummyUI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()=>View();
    }
}