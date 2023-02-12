using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Projet2.Controllers
{
    public class ActivitiesController : Controller
    {
        private Dal dal;
        private IWebHostEnvironment _webEnv;
        public ActivitiesController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
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
            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            string filePath = Path.Combine(uploads, activitiesVM.Activity.ActivityImage.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))

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
                activitiesVM.Activity.ActivityEventType,
                activitiesVM.Account.Username,
                "/images/" + activitiesVM.Activity.ActivityImage.FileName
                );

                List<Activity> CatActivities = new List<Activity>();
                CatActivities = dal.GetActivities();
                activitiesVM.activities = CatActivities;

                return RedirectToAction("CatalogueActivities", activitiesVM);
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
                    List <Activity> CatActivities= new List<Activity>();
                    CatActivities = dal.GetActivities();
                    activitiesVM.activities = CatActivities;
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
