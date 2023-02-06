using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult CreateStuff(int accountId, int inventoryId)
        {
            //StuffViewModel model = new StuffViewModel();
            //model.InventoryId = inventoryId;
            //model.AccountId = accountId;

            Stuff Stuff = new Stuff();
            Stuff.InventoryBorrowerId = inventoryId;
            Stuff.AccountOwnerId = accountId ;

            
            return View(Stuff);
        }

        [HttpPost]
        public IActionResult CreateStuff(Stuff model)
        { 
            if (ModelState.IsValid)
            {
                Stuff stuffCreated = dal.CreateStuff(model);
                return View("Index");
            }
            return View();
        }

        public IActionResult StuffCatalog()
        {
            List<Stuff> listStuff = dal.GetStuffs();
            return View(listStuff);
        }

    }
}
