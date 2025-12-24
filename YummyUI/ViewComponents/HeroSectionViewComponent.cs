using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class HeroSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}