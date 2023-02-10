﻿using Microsoft.AspNetCore.Authentication;
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
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);

                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateStuff(StuffViewModel model)
        {
            
            if (model.Authentificate == true)
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
                dal.EditStuffCreate(model.Stuff.Id, model.Account.Id);

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

        public IActionResult EditStuff(int id)
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                model.Stuff = dal.GetOneStuff(id);

                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditStuff(StuffViewModel model, int id)
        {

            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);

                model.Account = dal.GetAccount(accountId);
                
                dal.EditStuff(id, model.Stuff.Name, model.Stuff.Description, model.Stuff.Type, model.Stuff.State);

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

        public IActionResult RemoveStuff(int id)
        {
            ProfileViewModel model = new ProfileViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                model.Stuff = dal.GetOneStuff(id);

            }

            return View(model);

        }

        [HttpPost]
        public IActionResult RemoveStuff(ProfileViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);

                dal.RemoveStuff(id);

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

        public IActionResult StuffCatalog()
        {
            
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            
            if (model.Authentificate == true) {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
            if (model.Account != null)
            {
                Account account = model.Account;
                model.stuffs = dal.GetStuffs();
                List<Stuff> listStuff = model.stuffs.ToList();
                return View(model);
            }
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        public IActionResult CreateBookStuff(int id)
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
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
        public IActionResult CreateBookStuff(StuffViewModel model, int id)
        {
            if (model.Authentificate == true)
            {

                string accountId = (HttpContext.User.Identity.Name);

                model.ReservationStuff.StuffId = model.Stuff.Id;
                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;
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
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {

                string accountId = (HttpContext.User.Identity.Name);
                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;

                model.ReservationStuff = dal.GetReservations().Where(r => r.StuffId == stuff.Id).FirstOrDefault();
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountBorrowerId).FirstOrDefault();

                //model.Account = dal.GetAccount(accountId);
                //Account userAccount = model.Account;
                //model.Stuff.AccountBorrowerId = userAccount.Id;
                //model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();

            }

            return View(model);

        }

        [HttpPost]
        public IActionResult AcceptationBookStuff(StuffViewModel model, int id)
        {
            if (model.Authentificate==true)
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

        public IActionResult CancelationBookStuff(int id)
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);

                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;

                model.ReservationStuff = dal.GetReservations().Where(r => r.StuffId == stuff.Id).FirstOrDefault();
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountBorrowerId).FirstOrDefault();

                //model.Account = dal.GetAccount(accountId);
                //Account userAccount = model.Account;
                //model.Stuff.AccountBorrowerId = userAccount.Id;
                //model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();
                //dal.EditStuffCancelation(id);

                //int borrowerId = (int)model.Stuff.AccountBorrowerId;
                //Account borrowerAccount = dal.GetAccount(borrowerId);
            }

            return View(model);

        }


        [HttpPost]
        public IActionResult CancelationBookStuff(StuffViewModel model, int id)
        {
            
            if (model.Authentificate == true)
            {

                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);

                dal.EditStuffCancelation(id);

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


        public IActionResult ConsultationBookStuff(int id)
        {
            ProfileViewModel model = new ProfileViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);

                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;

                model.ReservationStuff = dal.GetReservations().Where(r => r.StuffId == stuff.Id).FirstOrDefault();
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountBorrowerId).FirstOrDefault();

                

            }

            return View(model);

        }

        [HttpPost]
        public IActionResult ConsultationBookStuff(ProfileViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);

                dal.EditStuffCancelation(id);

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



        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin", "Login");
        }

        ////////////////////RND    

    }
}
