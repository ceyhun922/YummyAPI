using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents 
{
    public class ChefSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}