using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;

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
            ActivitiesViewModel activitiesVM = new ActivitiesViewModel();
            activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);
            return View(activitiesVM);
        }

        [HttpPost]

          public IActionResult CreateActivity(ActivitiesViewModel activitiesVM)
            {
            activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);
           activitiesVM.Activity= 
                dal.CreateNewActivity(
                activitiesVM.Activity.StartDate,
                activitiesVM.Activity.EndDate,
                activitiesVM.Activity.Description,
                activitiesVM.Activity.Place,
                activitiesVM.Activity.activityType,
                activitiesVM.Account.Username
                );
                return View("CatalogueActivities",activitiesVM);
            }


        public IActionResult CatalogueActivities()
        {
            ActivitiesViewModel activitiesVM = new ActivitiesViewModel();
            activitiesVM.Account = dal.GetAccount(HttpContext.User.Identity.Name);
           
          activitiesVM.activities=dal.GetActivities();
           
            return View(activitiesVM);
        }




        ////////////////////////END

    }
}
