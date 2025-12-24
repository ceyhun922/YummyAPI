using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class MenuSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}