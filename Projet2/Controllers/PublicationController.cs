using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;

namespace Projet2.Controllers
{
    public class PublicationController : Controller
    {
        private Dal dal;

        public PublicationController()
        {
            dal = new Dal();
        }
        public IActionResult PublicationWall()
        {
            List<Publication> listPubli = dal.GetPublications();
            return View(listPubli);
        }

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

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin", "Login");
        }
        /////////////////////////END
    }
    
}
