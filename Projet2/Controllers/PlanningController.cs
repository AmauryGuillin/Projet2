using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Projet2.Controllers
{
    /// <summary>
    /// Controller for Planning.
    /// </summary>
    public class PlanningController : Controller
    {

        private Dal dal;//An instance of the "Dal" class
        public PlanningController()
        {
            dal = new Dal();
        }

        /// <summary>
        /// Displays the view for the user's planning.
        /// </summary>
        /// <returns>Returns the planning view.</returns>
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
