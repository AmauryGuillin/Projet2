using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Projet2.Controllers
{
    public class PlanningController : Controller
    {

        private Dal dal;
        public PlanningController()
        {
            dal = new Dal();
        }
        public IActionResult PlanningView()
        {
            PlanningViewModel model = new PlanningViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                Account account = model.Account;
                model.slots = dal.GetSlots().Where(r => r.PlanningId == account.PlanningId).ToList();
                Slot slot = model.Slot;
                model.activities = dal.GetActivities();
                List<Activity> activities = model.activities;
                model.Profile = dal.GetProfiles().Where(r => r.Id == account.ProfileId).FirstOrDefault();
                Profile profile = model.Profile;
                return View(model);
            }
            
                return RedirectToAction("Login","Login");
            
        }
        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin", "Login");
        }

        /////////////////////////////END
    }
}
