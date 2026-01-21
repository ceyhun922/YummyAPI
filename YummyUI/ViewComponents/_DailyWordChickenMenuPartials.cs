using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class _DailyWordChickenMenuPartials : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}