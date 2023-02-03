using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;

namespace Projet2.Controllers
{
    public class StuffController : Controller
    {
        private Dal dal;
        public StuffController()
        {
            dal = new Dal();
        }
        public IActionResult CreateStuff()
        {


            return View("CreateStuff");
        }
    }
}
