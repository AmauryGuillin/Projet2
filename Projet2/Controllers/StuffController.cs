using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;

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

        [HttpPost]
        public IActionResult CreateStuff(Stuff stuff)
        {
            if (ModelState.IsValid)
            {
                Stuff stuffCreated = dal.CreateStuff(stuff.Name, stuff.Description, stuff.Type, stuff.State);

                return View("Index");
            }
            return View(stuff);
        }

    }
}
