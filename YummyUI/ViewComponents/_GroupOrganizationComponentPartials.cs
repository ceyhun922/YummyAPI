using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class _GroupOrganizationComponentPartials : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}