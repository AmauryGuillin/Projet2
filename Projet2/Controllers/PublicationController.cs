
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
    /// <summary>
    /// Controller for Publication.
    /// </summary>
    public class PublicationController : Controller
    {
        private Dal dal;//An instance of the "Dal" class

        private IWebHostEnvironment _webEnv;//An instance of the "IWebHostEnvironment" interface

        /// <summary>
        /// Initializes a new instance of the PublicationController class.
        /// </summary>
        /// <param name="environment">The application's WebHost environment.</param>
        public PublicationController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }

        /// <summary>
        /// Displays a catalog of available publication to users that are authenticated.
        /// </summary>
        /// <returns>Returns the PublicationWall view with a list of all the available stuff.</returns>
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

        /// <summary>
        /// Create a view to display a form to create a Publication object.
        /// </summary>
        /// <returns>An IActionResult representing the view</returns>
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

        /// <summary>
        /// Process an HTTP POST request to create a new Publication object.
        /// </summary>
        /// <param name="model">The PublicationViewModel containing the information about the new Publication object.</param>
        /// <returns>The ID of the stuff item whose reservation is being cancelled</returns>
        [HttpPost]
        public IActionResult CreatePublication(PublicationViewModel model)
        {
            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            string filePath = Path.Combine(uploads, model.Publication.PubliImage.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Publication.PubliImage.CopyTo(fileStream);
            }
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
                 
                    model.Publication = publi;
                return RedirectToAction("PublicationWall",model);
            }

            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Displays the details of a single publication.
        /// </summary>
        /// <param name="id">The ID of the publication to display.</param>
        /// <returns>Returns the view of the single post</returns>
        public IActionResult OnePublication(int id)
        {
            PublicationViewModel model = new PublicationViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            string accountId = (HttpContext.User.Identity.Name);
            Account userAccount= dal.GetAccount(accountId);
            if (userAccount!=null)
            {
                Publication publication = dal.GetOnePublication(id);
                 model.Publication= publication;
                model.Account= userAccount;
                model.Publication.Account = dal.GetAccounts().Where(r => r.Id == publication.AccountId).FirstOrDefault();
                
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

       

        /// <summary>
        /// Process an HTTP GET request to display the edit form of an existing Publication object.
        /// </summary>
        /// <param name="id">The identifier of the Publication object to modify.</param>
        /// <returns>Publication object edit view.</returns>
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

        /// <summary>
        /// Process an HTTP POST request to modify the information of an existing Publication object.
        /// </summary>
        /// <param name="model">A PublicationViewModel object containing the updated Publication object information.</param>
        /// <param name="id">The identifier of the Stuff object to modify.</param>
        /// <returns>The ID of the stuff item whose reservation is being cancelled</returns>
        [HttpPost]
        public IActionResult EditPublication(PublicationViewModel model, int id)
        {
            string imagepath;
            string accountId = (HttpContext.User.Identity.Name);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
            model.Account = dal.GetAccount(accountId);
            Publication publication = dal.GetOnePublication(id);
             
            string uploads = Path.Combine(_webEnv.WebRootPath, "images");

            if (model.Publication.PubliImage != null)
            {
                string filePath = Path.Combine(uploads, model.Publication.PubliImage.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Publication.PubliImage.CopyTo(fileStream);
                }
                imagepath = "/images/" + model.Publication.PubliImage.FileName;
            }
            else { imagepath = publication.ImagePath; }
            
                dal.EditPublication(id, model.Publication.Name, model.Publication.PublicationType,model.Publication.Content,model.Publication.Date,imagepath);
                return RedirectToAction("PublicationWall");
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Displays the confirmation page for removing a publication item
        /// </summary>
        /// <param name="id">The id of the publication item to be removed</param>
        /// <returns>The view to confirm the removal of the publication item</returns>
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

        /// <summary>
        /// Removes a publication item from the database
        /// </summary>
        /// <param name="model">The view model containing the account and publication item</param>
        /// <param name="id">The id of the publication item to be removed</param>
        /// <returns>The ID of the stuff item whose reservation is being cancelled</returns>
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


        /// <summary>
        /// Logs the user out by deleting the authentication cookies and redirects him to the login page.
        /// </summary> 
        /// <returns>Redirect to login page</returns>
        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin", "Login");
        }

    }

}
