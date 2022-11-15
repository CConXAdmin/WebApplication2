using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.Test.Controllers
{
    [Area("Test")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
