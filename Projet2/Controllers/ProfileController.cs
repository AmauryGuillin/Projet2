using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

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
               
                    Profile profile = dal.GetProfiles().Where(r => r.Id == id).FirstOrDefault();

                    
                    return View(profile);
                
            
        }



        [HttpPost]
        public IActionResult EditProfile(Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return View("EditProfile");
            }
            

              dal.EditProfile(profile);
          

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
