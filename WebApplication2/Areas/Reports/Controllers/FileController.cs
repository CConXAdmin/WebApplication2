using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class FileController : Controller
    {
        public IActionResult Index(bool nolayout = false)
        {
            if (nolayout) ViewBag.NoLayout = true;
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
    }
}
