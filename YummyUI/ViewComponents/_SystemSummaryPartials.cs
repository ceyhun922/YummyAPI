using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class _SystemSummaryPartials : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View();
        }
    }
}