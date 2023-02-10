using Microsoft.AspNetCore.Mvc;

namespace Projet2.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
