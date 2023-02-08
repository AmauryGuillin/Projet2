using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Xml.Linq;

namespace Projet2.Controllers
{
    public class StuffController : Controller
    {
        private Dal dal;

        public StuffController()
        {
            dal = new Dal();
        }
        //private IWebHostEnvironment _webEnv;
        //public StuffController(IWebHostEnvironment environment)
        //{
        //    _webEnv = environment;
        //    this.dal = new Dal();
        //}

        public IActionResult CreateStuff()
        {
            ProfileViewModel model = new ProfileViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);

                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateStuff(ProfileViewModel model)
        {

            if (ModelState.IsValid)
            {
                string accountId = (HttpContext.User.Identity.Name);

                //string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                //string filePath = Path.Combine(uploads, model.StuffImage.FileName);
                //using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    model.StuffImage.CopyTo(fileStream);
                //}
                //dal.CreateStuff(
                //    model.Name, "/images/" + model.StuffImage.FileName, model.Type, model.State);
                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;
                Stuff stuffCreated = dal.CreateStuff(model.Stuff);
                dal.EditStuff(model.Stuff.Id, model.Account.Id);

                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }    
            }
            return View();
        }

        public IActionResult StuffCatalog()
        {
            List<Stuff> listStuff = dal.GetStuffs();
            return View(listStuff);
        }

        public IActionResult CreateBookStuff(int id)
        {
            ProfileViewModel model = new ProfileViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {

                string accountId = (HttpContext.User.Identity.Name);
                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;
                
                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;
                model.Stuff.AccountBorrowerId = userAccount.Id;
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();

                return View(model);
            }

            return View(model);

        }

        [HttpPost]
        public IActionResult CreateBookStuff(ProfileViewModel model, int id)
        {
            if (ModelState.IsValid)
            {

                model.ReservationStuff.StuffId = model.Stuff.Id;
                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;

                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;

                //Sert à modifier l'accountBorowerId
                dal.EditStuffReservation(model.Stuff.Id, model.Account.Id);
                ReservationStuff reservationCreated = dal.CreateReservationStuff(model.ReservationStuff);

                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }
                else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }
            }

            return View();
        }

        public IActionResult AcceptationBookStuff(int id)
        {
            ProfileViewModel model = new ProfileViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);

                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;

                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;
                model.Stuff.AccountBorrowerId = userAccount.Id;
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();

            }

            return View(model);

        }

        [HttpPost]
        public IActionResult AcceptationBookStuff(ProfileViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);

                dal.EditStuffAcceptation(id);

                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }
                else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }
            }

            return View();

        }
    }
}
