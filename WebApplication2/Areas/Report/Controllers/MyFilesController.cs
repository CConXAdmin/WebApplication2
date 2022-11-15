using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.Report.Controllers
{
    [Area("Report")]
    public class MyFilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index1()
        {
            return View();
        }
    }
}
