using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class GallerySectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}