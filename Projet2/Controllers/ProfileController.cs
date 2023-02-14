using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Projet2.Models.Informations;
using Projet2.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Projet2.Controllers
{
    /// <summary>
    /// Controller for Profile.
    /// </summary>
    public class ProfileController : Controller
    {

        private Dal dal;//An instance of the "Dal" class

        private IWebHostEnvironment _webEnv;//An instance of the "IWebHostEnvironment" interface

        /// <summary>
        /// Initializes a new instance of the ProfileController class.
        /// </summary>
        /// <param name="environment">The application's WebHost environment.</param>
        public ProfileController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }

        /// <summary>
        /// Action method to display the view to edit a profile
        /// </summary>
        /// <param name="id">The id of the profile to edit</param>
        /// <returns>The view to edit a profile</returns>
        public IActionResult EditProfile(int id)
        {
            ProfileViewModel profilevm=new ProfileViewModel();
            profilevm.Profile= dal.GetProfiles().Where(r => r.Id == id).FirstOrDefault();
            profilevm.Account=dal.GetAccounts().Where(r => r.ProfileId==id).FirstOrDefault();
            Account accountUser = profilevm.Account;
            profilevm.Contact = dal.GetContacts().Where(r => r.Id == accountUser.ContactId).FirstOrDefault();
            profilevm.Infos= dal.GetInformations().Where(r => r.Id == accountUser.InfoPersoId).FirstOrDefault();
            Inventory inventory = profilevm.Inventory;
            profilevm.Inventory = dal.GetInventories().Where(r => r.Id == accountUser.InventoryId).FirstOrDefault();
            //profilevm.inventory.nbStuff=dal.GetInventoryContent().Count();
            return View(profilevm);
        }

        /// <summary>
        /// Action to edit a user profile
        /// </summary>
        /// <param name="profilevm">Profile view model containing the user's profile, contact, information and inventory</param>
        /// <returns>Returns the user's profile view</returns>
        [HttpPost]
        public IActionResult EditProfile(ProfileViewModel profilevm)
        {
            profilevm.Account = dal.GetAccounts().Where(r => r.Id == profilevm.Account.Id).FirstOrDefault();
            dal.EditProfile(profilevm.Profile);
            dal.EditContact(profilevm.Contact);
            dal.EditInfos(profilevm.Infos);
            dal.EditInventory(profilevm.Inventory);
            return View("ProfileView",profilevm);
        }

        /// <summary>
        /// Displays the view for editing the user's profile picture.
        /// </summary>
        /// <returns>The EditProfilePIC view with an empty ProfileViewModel.</returns>
        public IActionResult EditProfilePIC()
        {
            ProfileViewModel profilevm = new ProfileViewModel();
            return View(profilevm);
        }

        /// <summary>
        /// Updates the profile picture of the current user with the uploaded image.
        /// </summary>
        /// <param name="profilevm">The ProfileViewModel containing the profile and the uploaded image.</param>
        /// <returns>The updated profile view.</returns>
        [HttpPost]
        public IActionResult EditProfilePIC(ProfileViewModel profilevm)
        {
            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            string filePath = Path.Combine(uploads, profilevm.Profile.ProfilImage.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            { profilevm.Profile.ProfilImage.CopyTo(fileStream); }
            dal.EditProfilePIC("/images/" + profilevm.Profile.ProfilImage.FileName,profilevm.Profile.Id);
            return View(profilevm);
        }

        public IActionResult ProfileView()
        {
            //    ProfileViewModel pvm= new ProfileViewModel();
            //    pvm.Account = dal.GetAccounts().Where(r => r.Id == pvm.Account.Id).FirstOrDefault();
            return View();

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
