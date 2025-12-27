using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class _AdminSettingsComponentPartilas : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}