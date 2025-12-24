using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class TestimonialSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}