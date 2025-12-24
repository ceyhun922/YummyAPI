using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class WhyUsSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}