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
            PlanningViewModel model = new PlanningViewModel();
            model.Account= dal.GetAccount(HttpContext.User.Identity.Name);
            Account account = model.Account;
            //   model.slots = (System.Collections.Generic.List<Slot>)dal.GetSlots().Where(r => r.PlanningId == model.Account.PlanningId);
            model.slots=dal.GetSlots().Where(r => r.PlanningId == account.PlanningId).ToList();
            Slot slot = model.Slot;
            model.activities = dal.GetActivities();
            List<Activity> activities = model.activities;
            // int ProfileId = (int)account.ProfileId;
            //model.Profile= dal.GetProfiles().Where(r=> r.Id== ProfileId).FirstOrDefault();
            model.Profile=dal.GetProfiles().Where(r => r.Id== account.ProfileId).FirstOrDefault();
            Profile profile = model.Profile;
            return View(model);
        }
    }
}
