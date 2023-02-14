using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.Models.Informations;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.IO;
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
                evm.Account = dal.GetAccount(accountUser.Id);
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

        public IActionResult ProfileViewParamsEmployee()
        {

            EmployeeViewModel evm = new EmployeeViewModel  { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            string accountId = (HttpContext.User.Identity.Name);
            Account account = dal.GetAccount(accountId);

            if (account != null)
            {
                evm.Account = account;
                evm.Profile = dal.GetProfiles().Where(r => r.Id == account.ProfileId).FirstOrDefault();
                evm.Contact = dal.GetContacts().Where(r => r.Id == account.ContactId).FirstOrDefault();
                evm.Infos = dal.GetInformations().Where(r => r.Id == account.InfoPersoId).FirstOrDefault();
                return View(evm);
            }

            return RedirectToAction("Login", "Login");
        }


        [HttpPost]
        public IActionResult ProfileViewParamsEmployee(EmployeeViewModel evm)
        {
            string imagepath;
            string accountId = (HttpContext.User.Identity.Name);
            Account account = dal.GetAccount(accountId);
            if (account != null)
            {
                Contact contact = dal.GetContacts().Where(r => r.Id == account.ContactId).FirstOrDefault();
                InfoPerso infos = dal.GetInformations().Where(r => r.Id == account.InfoPersoId).FirstOrDefault();
                Profile profile = dal.GetProfiles().Where(r => r.Id == account.ProfileId).FirstOrDefault();


                string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                if (evm.Profile.ImagePath != null)
                {
                    string filePath = Path.Combine(uploads, evm.Profile.ProfilImage.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        evm.Profile.ProfilImage.CopyTo(fileStream);
                    }
                    imagepath = "/images/" + evm.Profile.ProfilImage.FileName;
                }
                else { imagepath = profile.ImagePath; }

                evm.Account = dal.EditAccount(
                        account.Id,
                        evm.Account.Username,
                        evm.Account.Password
                        );
                evm.Profile = dal.EditProfileS(
                        profile.Id,
                        imagepath,
                         evm.Profile.Games,
                         evm.Profile.Bio
                        );
                evm.Contact = dal.EditContacts(
                        contact.Id,
                        evm.Contact.EmailAdress,
                        evm.Contact.TelephoneNumber
                        );

                return RedirectToAction("ProfileViewEmployee", evm);
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
