using Microsoft.AspNetCore.Mvc;

namespace YummyUI.ViewComponents
{
    public class _GroupRezervationTablePartials : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();   
        }
    }
}