using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class ContactSectionViewComponent : ViewComponent
    {
         public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}