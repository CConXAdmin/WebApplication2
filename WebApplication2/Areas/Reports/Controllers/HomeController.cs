using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class HomeController : Controller
    {
        public IActionResult Index(bool nolayout = false)
        {
            if (nolayout) ViewBag.NoLayout = true;
            return View();
        }
    }
}
