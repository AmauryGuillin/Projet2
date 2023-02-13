using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Projet2.Controllers
{
    public class EmployeeController : Controller
    {
        private Dal dal;
        private IWebHostEnvironment _webEnv;
        public EmployeeController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }
        public IActionResult ProfileViewEmployee()
        {
            EmployeeViewModel evm = new EmployeeViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            Account accountUser = dal.GetAccount(HttpContext.User.Identity.Name);
            evm.Account = accountUser;
            if (accountUser != null)
            {

                evm.Profile = dal.GetProfiles().Where(r => r.Id == accountUser.ProfileId).FirstOrDefault();
                evm.Infos = dal.GetInformations().Where(r => r.Id == accountUser.InfoPersoId).FirstOrDefault();
                evm.Contact = dal.GetContacts().Where(r => r.Id == accountUser.ContactId).FirstOrDefault();
                evm.Stuffs = dal.GetOwnedStuff(accountUser.Id);
                List<Stuff> Stuffs = evm.Stuffs;
                evm.ReservationStuffs = dal.GetReservations();
                List<ReservationStuff> ListReservations = evm.ReservationStuffs;

                IEnumerable<Activity> lastactivities = dal.GetActivities();
                evm.Activities = lastactivities.Reverse<Activity>().Take(3);

                IEnumerable<Publication> lastpublications = dal.GetPublications();
                evm.Publications = lastpublications.Reverse<Publication>().Take(3);

                foreach (var Publication in evm.Publications)
                {
                    Account AuthorPubli = dal.GetAccounts().Where(r => r.Id == Publication.AccountId).FirstOrDefault();
                    if (AuthorPubli != null) { Publication.Account = AuthorPubli; }
                }

                return View(evm);
            }

            return RedirectToAction("Login", "Login");

        }



        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }


        /////////////////////END
    }
}
