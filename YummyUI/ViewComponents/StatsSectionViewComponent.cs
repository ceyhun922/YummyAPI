using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class StatsSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}