using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class _MenuBreakfastSectionPartials : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}