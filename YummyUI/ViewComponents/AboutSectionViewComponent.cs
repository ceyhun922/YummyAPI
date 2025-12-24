using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class AboutSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}