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
            profilevm.infos= dal.GetInformations().Where(r => r.Id == accountUser.InfoPersoId).FirstOrDefault();
            Inventory inventory = profilevm.inventory;
            profilevm.inventory = dal.GetInventories().Where(r => r.Id == accountUser.InventoryId).FirstOrDefault();
            //profilevm.inventory.nbStuff=dal.GetInventoryContent().Count();
            return View(profilevm);
        }



        [HttpPost]
        public IActionResult EditProfile(ProfileViewModel profilevm)
        {
            
            profilevm.account= dal.GetAccounts().Where(r => r.Id == profilevm.account.Id).FirstOrDefault();
            dal.EditProfile(profilevm.profile);
            dal.EditContact(profilevm.contact);
            dal.EditInfos(profilevm.infos);
            dal.EditInventory(profilevm.inventory);
            return View("ProfileView",profilevm);
        }
    


        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin","Login");
        }


       


        //////////////////////////////END
    }
}
