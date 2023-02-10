using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Projet2.Controllers
{
    public class ActivitiesController : Controller
    {
        private Dal dal;
        public ActivitiesController()
        {
            dal = new Dal();
        }
        public IActionResult CreateActivity()
        {
            ActivitiesViewModel activitiesVM = new ActivitiesViewModel
            { Authentificate = HttpContext.User.Identity.IsAuthenticated};
            if (activitiesVM.Authentificate == true) { 
            activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);
            return View(activitiesVM); 
            }
            else { return RedirectToAction("Login", "Login"); }
        }

        [HttpPost]

        public IActionResult CreateActivity(ActivitiesViewModel activitiesVM)
        {

           
            activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);
            if (activitiesVM.Account != null)
            {
                activitiesVM.Activity =
                dal.CreateNewActivity(
                activitiesVM.Activity.StartDate,
                activitiesVM.Activity.EndDate,
                activitiesVM.Activity.Description,
                activitiesVM.Activity.Place,
                activitiesVM.Activity.activityType,
                activitiesVM.Account.Username
                );
                return View("CatalogueActivities", activitiesVM);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }


    public IActionResult CatalogueActivities()
        {
            ActivitiesViewModel activitiesVM = new ActivitiesViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (activitiesVM.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                activitiesVM.Account = dal.GetAccount(accountId);
                Account account = activitiesVM.Account;
                if (account != null)
                {
                    activitiesVM.activities = dal.GetActivities();

                    return View(activitiesVM);
                }
            }
            
                return RedirectToAction("Login", "Login");
          
        }

        
        //public  IActionResult Book ( int id)
        //{
        //    ActivitiesViewModel activitiesVM = new ActivitiesViewModel();
        //    activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);
        //    Account account = activitiesVM.Account;
        //    activitiesVM.activities = dal.GetActivities();
        //    Activity selected = dal.GetActivities().Where(a => a.Id == id).FirstOrDefault();
            

            
        //    return View("CatalogueActivities");
        //}
       
        public IActionResult Book( int id)
        {

            ActivitiesViewModel activitiesVM = new ActivitiesViewModel();
            activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);
            Account account = activitiesVM.Account;
            int planningid= (int)account.PlanningId;
            activitiesVM.activities = dal.GetActivities();
            List<Activity> activities = activitiesVM.activities;
            Activity selected = dal.GetActivities().Where(a => a.Id == id).FirstOrDefault();
            activitiesVM.Slot = dal.CreateSlot
                    (
                        selected.StartDate,
                        selected.EndDate,
                        selected.Id,
                        planningid
                    );
                    dal.AddSlotToPlanning(account.Id, activitiesVM.Slot);
                    return RedirectToAction("PlanningView","Planning");
        }




        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin", "Login");
        }

        ////////////////////////END

    }
}
