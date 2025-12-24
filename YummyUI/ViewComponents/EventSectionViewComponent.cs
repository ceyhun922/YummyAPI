using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class EventSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
   
}