using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class _AdminHeadComponentPartilas : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
    
}