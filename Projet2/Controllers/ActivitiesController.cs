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
    /// <summary>
    /// Controller for activities.
    /// </summary>
    public class ActivitiesController : Controller
    {
        private Dal dal;//An instance of the "Dal" class

        private IWebHostEnvironment _webEnv;//An instance of the "IWebHostEnvironment" interface

        /// <summary>
        /// Initializes a new instance of the ActivitiesController class.
        /// </summary>
        /// <param name="environment">The application's WebHost environment.</param>
        public ActivitiesController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }

        /// <summary>
        /// Create a new activity.
        /// </summary>
        /// <returns>A view for creating an activity</returns>
        public IActionResult CreateActivity()
        {
            ActivitiesViewModel activitiesVM = new ActivitiesViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);
            Account account = activitiesVM.Account;
            if (account!=null)
            {
                if (account.role == Projet2.Models.Role.Admin || account.role == Projet2.Models.Role.Salarie)
                {
                    return View(activitiesVM);
                }
                else { RedirectToAction("Index", "Login"); }
               
            }
            return RedirectToAction("Login", "Login");
        }



        /// <summary>
        /// Processes POST data from the create view of an activity.
        /// </summary>
        /// <param name="activitiesVM">The data of the activity to create.</param>
        /// <returns>Redirects to the activity catalog page if the activity was created successfully</returns>
        [HttpPost]
        public IActionResult CreateActivity(ActivitiesViewModel activitiesVM)
        {
            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            string filePath = Path.Combine(uploads, activitiesVM.Activity.ActivityImage.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                activitiesVM.Activity.ActivityImage.CopyTo(fileStream);
            }


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
                "/images/" + activitiesVM.Activity.ActivityImage.FileName,
                activitiesVM.Account.Id,
                activitiesVM.Activity.NumberOfParticipants
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

        /// <summary>
        /// Displays the activity catalog for the authenticated user.
        /// </summary>
        /// <returns>Returns the activity catalog view if the user is authenticated, otherwise redirects to the login page.</returns>
        public IActionResult CatalogueActivities()
        {
            ActivitiesViewModel activitiesVM = new ActivitiesViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            Account account = dal.GetAccount(HttpContext.User.Identity.Name);
            activitiesVM.Account=account;
            if (account != null)
            {
                    List <Activity> CatActivities= new List<Activity>();
                    CatActivities = dal.GetActivities();
                    activitiesVM.activities = CatActivities;
                    return View(activitiesVM);
                
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



        /// <summary>
        /// Reserves an activity for the authenticated user, i.e. registers for the event.
        /// </summary>
        /// <param name="id">The ID of the activity to book, to register.</param>
        /// <returns>Redirects to the authenticated user's schedule view.</returns>
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

        /// <summary>
        /// Deletes an activity and all associated time slots from the provided activity ID.
        /// </summary>
        /// <param name="id">The ID of the activity to delete.</param>
        /// <returns>Returns the activity catalog view if the user is authenticated, otherwise redirects to the login page.</returns>
        public IActionResult DeleteActivity(int id)
        {

            ActivitiesViewModel activitiesVM = new ActivitiesViewModel();
            activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);

            Account account = activitiesVM.Account;

            if (account != null)
            {
            int planningid = (int)account.PlanningId;
             Planning planning = account.Planning;
            activitiesVM.activities = dal.GetActivities();
            List<Activity> activities = activitiesVM.activities;
            Activity selected = dal.GetActivities().Where(a => a.Id == id).FirstOrDefault();
            List<Slot> selectedSlots = dal.GetSlots().Where(a => a.ActivityId ==selected.Id ).ToList();
            activitiesVM.slots = selectedSlots;
                foreach ( var slot in selectedSlots )
                {
                    dal.RemoveSlot(slot.Id);
                }
                dal.RemoveActivity(selected.Id);
                   
            return RedirectToAction("CatalogueActivities", activitiesVM);
        }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Displays the edit page of an activity.
        /// </summary>
        /// <param name="id">The identifier of the activity to edit.</param>
        /// <returns>The view containing the activity edit form.</returns>
        public IActionResult EditActivity(int id)
        {

            ActivitiesViewModel activitiesVM = new ActivitiesViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);

            Account account = activitiesVM.Account;

            if (account != null)
            {
                int planningid = (int)account.PlanningId;
                Planning planning = account.Planning;
                activitiesVM.activities = dal.GetActivities();
                List<Activity> activities = activitiesVM.activities;
                Activity selected = dal.GetActivities().Where(a => a.Id == id).FirstOrDefault();
                activitiesVM.Activity = selected; 
                List<Slot> selectedSlots = dal.GetSlots().Where(a => a.ActivityId == selected.Id).ToList();
                activitiesVM.slots = selectedSlots;
                //foreach (var slot in selectedSlots)
                //{
                //    dal.EditSlot(slot.Id,
                //        activitiesVM.Activity.StartDate,
                //        activitiesVM.Activity.EndDate
                //        );
                //}
                //dal.EditActivity(selected.Id,
                //    activitiesVM.Activity.StartDate,
                //    activitiesVM.Activity.EndDate,
                //    activitiesVM.Activity.Theme,
                //    activitiesVM.Activity.Description,
                //    activitiesVM.Activity.Place,
                //    activitiesVM.Activity.ImagePath
                    
                //    );

                return View(activitiesVM);
            }
            return RedirectToAction("PlanningView", "Planning");
        }

        /// <summary>
        /// Action called when the user submits the edit form of an activity.
        /// </summary>
        /// <param name="activitiesVM">The view model representing the activity to edit.</param>
        /// <param name="id">The identifier of the activity to edit</param>
        /// <returns>Redirects to the activity catalog page if the user is authenticated, otherwise redirects to the login page</returns>
        [HttpPost]
        public IActionResult EditActivity(ActivitiesViewModel activitiesVM, int id)
        {
            string imagepath;
            Account account = dal.GetAccount(HttpContext.User.Identity.Name);
            

            if (account != null)
            {
                activitiesVM.Account = account;
                int planningid = (int)account.PlanningId;
                Planning planning = account.Planning;
                activitiesVM.activities = dal.GetActivities();
                List<Activity> activities = activitiesVM.activities;
                List<Slot> selectedSlots = dal.GetSlots().Where(a => a.ActivityId == activitiesVM.Activity.Id).ToList();
                Activity activity =dal.GetActivities().Where(r=>r.Id==id).FirstOrDefault();
                activitiesVM.slots = selectedSlots;

                string uploads = Path.Combine(_webEnv.WebRootPath, "images");

                if (activitiesVM.Activity.ActivityImage != null)
                {
                    string filePath = Path.Combine(uploads, activitiesVM.Activity.ActivityImage.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        activitiesVM.Activity.ActivityImage.CopyTo(fileStream);
                    }
                    imagepath = "/images/" + activitiesVM.Activity.ActivityImage.FileName;
                }
                else { imagepath = activity.ImagePath; }



                foreach (var slot in selectedSlots)
                {
                    dal.EditSlot(
                        slot.Id,
                        activitiesVM.Activity.StartDate,
                        activitiesVM.Activity.EndDate
                        );
                }
                dal.EditActivity(id,
                    activitiesVM.Activity.StartDate,
                    activitiesVM.Activity.EndDate,
                    activitiesVM.Activity.Theme,
                    activitiesVM.Activity.Description,
                    activitiesVM.Activity.Place,
                    imagepath,
                    activitiesVM.Activity.NumberOfParticipants

                    ); ;

                return RedirectToAction("CatalogueActivities", activitiesVM);
            }
            return RedirectToAction("login", "login");
        }


        /// <summary>
        /// Logs the user out by deleting the authentication cookies and redirects him to the login page.
        /// </summary> 
        /// <returns>Redirect to login page</returns>
        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
    }
}
