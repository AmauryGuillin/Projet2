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
    public class ProfileController : Controller
    {

        private Dal dal;
        private IWebHostEnvironment _webEnv;
        public ProfileController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }
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

        public IActionResult EditProfilePIC()
        {
            ProfileViewModel profilevm = new ProfileViewModel();
            return View();
        }

        [HttpPost]
        public IActionResult EditProfilePIC(ProfileViewModel profilevm)
        {
            
                    string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                    string filePath = Path.Combine(uploads, profilevm.Profile.ProfilImage.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        profilevm.Profile.ProfilImage.CopyTo(fileStream);
                    }
                    dal.EditProfilePIC("/images/" + profilevm.Profile.ProfilImage.FileName,profilevm.Profile.Id);
              
            return View();
        }




        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin","Login");
        }

        public IActionResult ProfileView()
        {
            //    ProfileViewModel pvm= new ProfileViewModel();
            //    pvm.Account = dal.GetAccounts().Where(r => r.Id == pvm.Account.Id).FirstOrDefault();
            return View();

        }

       


        //////////////////////////////END
    }
}
