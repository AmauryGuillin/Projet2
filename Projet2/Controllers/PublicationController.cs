
﻿using Microsoft.AspNetCore.Http;

﻿using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Projet2.Controllers
{
    public class PublicationController : Controller
    {
        private Dal dal;
        private IWebHostEnvironment _webEnv;

        public PublicationController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }
        public IActionResult PublicationWall(PublicationViewModel model)
        {

            model.Publications = dal.GetPublications();
            List<Publication> publications = model.Publications;
            //Publication publication = model.Publication;
            List<Account> accounts = dal.GetAccounts();
            //foreach (var publication in publications)
            //{
            //    var account = accounts.Where(r => r.Id == publication.AccountId).FirstOrDefault();
            //    publication.Account = account;
            //}
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
            PublicationViewModel model = new PublicationViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public IActionResult CreatePublication(PublicationViewModel model)
        {
            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            string filePath = Path.Combine(uploads, model.Publication.PubliImage.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))

                if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;
                Publication publi = dal.CreatePublication(
                "/images/" + model.Publication.PubliImage.FileName,
                userAccount.Id,
                model.Publication.Content,
                model.Publication.Name,
                model.Publication.PublicationType
                    );
                    //dal.EditCreatePublication(model.Publication.Id, model.Account.Id);
                    model.Publication = publi;
                return RedirectToAction("PublicationWall",model);
            }

            return RedirectToAction("Login", "Login");
        }


        public IActionResult OnePublication(int id)
        {
            PublicationViewModel model = new PublicationViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true) 
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Publication = dal.GetOnePublication(id);
                Publication publication = model.Publication;
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult OnePublication(PublicationViewModel model, int id)
        {
            
            if (model.Authentificate == true)
            { 
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                return View("EditPublication");
            }
            return RedirectToAction("Login", "Login");
        }

        public IActionResult EditPublication(int id)
        {
            PublicationViewModel model = new PublicationViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                model.Publication = dal.GetOnePublication(id);
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult EditPublication(PublicationViewModel model, int id)
        {
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                dal.EditPublication(id, model.Publication.Name, model.Publication.PublicationType,model.Publication.Content,model.Publication.Date);
                return RedirectToAction("PublicationWall");
            }
            return RedirectToAction("Login", "Login");
        }

        

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin", "Login");
        }
        /////////////////////////END

    }

}
