using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.Report.Controllers
{
    [Area("Report")]
    public class FilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
