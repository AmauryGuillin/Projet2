
﻿using Microsoft.AspNetCore.Http;

﻿using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Projet2.Controllers
{
    public class PublicationController : Controller
    {
        private Dal dal;

        public PublicationController()
        {
            dal = new Dal();
        }
        public IActionResult PublicationWall(PublicationViewModel model)
        {

            model.Publications = dal.GetPublications();
            List<Publication> publications = model.Publications;

            //Publication publication = model.Publication;

            List<Account> accounts = dal.GetAccounts();

            foreach (var publication in publications)
            {
                var account = accounts.Where(r => r.Id == publication.AccountId).FirstOrDefault();
                publication.Account = account;
            }

            return View(model);
        }

        //public IActionResult PublicationWall()
        //{
        //    var model = new PublicationViewModel();
        //    model.Publications = dal.GetPublications();
        //    var accounts = dal.GetAccounts().ToDictionary(x => x.Id, x => x);

        //    foreach (var publication in model.Publications)
        //    {
        //        publication.Account = accounts[publication.AccountId];
        //    }

        //    return View(model);
        //}


        public IActionResult CreatePublication()
        {
            PublicationViewModel model = new PublicationViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);

                return View(model);
            }
            return View(model);

        }
        [HttpPost]
        public IActionResult CreatePublication(PublicationViewModel model)
        {

            if (ModelState.IsValid)
            {

                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;
                Publication publi = dal.CreatePublication(model.Publication);
                dal.EditCreatePublication(model.Publication.Id, model.Account.Id);


                return RedirectToAction("PublicationWall");

            }

            return RedirectToAction("PublicationWall");
        }


        public IActionResult OnePublication(int id)
        {
            PublicationViewModel model = new PublicationViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);

                model.Publication = dal.GetOnePublication(id);
                Publication publication = model.Publication;

            }

            return View(model);

        }

        [HttpPost]
        public IActionResult OnePublication(PublicationViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);

                return View("EditPublication");
            }

            return View();
        }

        public IActionResult EditPublication(int id)
        {
            PublicationViewModel model = new PublicationViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                model.Publication = dal.GetOnePublication(id);

                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPublication(PublicationViewModel model, int id)
        {
            if (ModelState.IsValid)
            {

                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                
                dal.EditPublication(id, model.Publication.Name, model.Publication.PublicationType,model.Publication.Content,model.Publication.Date);


                return RedirectToAction("PublicationWall");

            }

            return RedirectToAction("PublicationWall");

        }

        



        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin", "Login");
        }
        /////////////////////////END

    }

}
