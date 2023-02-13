
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
        public IActionResult PublicationWall()
        {
            PublicationViewModel model = new PublicationViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            string accountId = (HttpContext.User.Identity.Name);
            model.Account = dal.GetAccount(accountId);
            Account account= model.Account;
            if (account !=null)
            {
             List<Publication> publications = dal.GetPublications();
             model.Publications= publications;
             foreach (var publication in publications) {
               model.Publication= publication;
               model.Publication.Account = dal.GetAccounts().Where(r => r.Id == publication.Account.Id).FirstOrDefault();
               Account Author = model.Publication.Account;    
                }

                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }


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
                model.Publication.PublicationType,
                model.Publication.Date
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
            string accountId = (HttpContext.User.Identity.Name);
            Account userAccount= dal.GetAccount(accountId);
            if (userAccount!=null)
            {
                model.Publication = dal.GetOnePublication(id);
                Publication publication = model.Publication;
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        //[HttpPost]
        //public IActionResult OnePublication(PublicationViewModel model, )
        //{
        //    string accountId = (HttpContext.User.Identity.Name);
        //    Account account = dal.GetAccount(accountId);
        //    if (account!=null)
        //    {
        //        model.Publication = dal.GetOnePublication(id);
        //        Publication publication = model.Publication;
        //        return View("EditPublication", model);
        //    }
        //    return RedirectToAction("Login", "Login");
        //}

        public IActionResult EditPublication(int id)
        {
            PublicationViewModel model = new PublicationViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated == true)
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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                dal.EditPublication(id, model.Publication.Name, model.Publication.PublicationType,model.Publication.Content,model.Publication.Date);
                return RedirectToAction("PublicationWall");
            }
            return RedirectToAction("Login", "Login");
        }

        public IActionResult RemovePublication(int id)
        {
            PublicationViewModel model = new PublicationViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                model.Publication = dal.GetOnePublication(id);
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult RemovePublication(PublicationViewModel model, int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                dal.RemovePublication(id);
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
