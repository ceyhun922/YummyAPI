using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class BookTableSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}