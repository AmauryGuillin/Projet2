using Microsoft.AspNetCore.Mvc;

namespace Projet2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
