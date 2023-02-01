using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Projet2.Models.Informations;
using Projet2.ViewModels;

namespace Projet2.Controllers
{
    public class ProfileController : Controller
    {

        private Dal dal;
        public ProfileController()
        {
            dal = new Dal();
        }
        public IActionResult EditProfile(int id)
        {
            ProfileViewModel profilevm=new ProfileViewModel();
            profilevm.profile= dal.GetProfiles().Where(r => r.Id == id).FirstOrDefault();
            profilevm.account=dal.GetAccounts().Where(r => r.ProfileId==id).FirstOrDefault();
            Account accountUser = profilevm.account;
            profilevm.contact = dal.GetContacts().Where(r => r.Id == accountUser.ContactId).FirstOrDefault();

            return View(profilevm);
        }



        [HttpPost]
        public IActionResult EditProfile(Profile profile, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View("EditProfile");
            }

              dal.EditProfile(profile);
            
            dal.EditContact(contact);
            return View("ProfileView");
        }
    


        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin","Login");
        }





    //////////////////////////////END
    }
}
