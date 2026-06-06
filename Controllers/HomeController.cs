using Microsoft.AspNetCore.Mvc;

namespace WorldCup2026.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}