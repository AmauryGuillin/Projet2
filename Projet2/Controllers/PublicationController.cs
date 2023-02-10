using Microsoft.AspNetCore.Http;
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

        public IActionResult RemovePublication(int id)
        {
            PublicationViewModel model = new PublicationViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                model.Publication = dal.GetOnePublication(id);

            }

            return View(model);

        }

        [HttpPost]
        public IActionResult RemoveStuff(PublicationViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);

                dal.RemovePublication(id);

                return View("PublicationWall");
            }

            return View();
        }


    }

}
