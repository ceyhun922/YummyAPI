using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class _ContactFormPartials : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}