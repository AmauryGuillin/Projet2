using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projet2.Models;
using Projet2.Models.Informations;
using Projet2.Models.Messagerie;
using Projet2.Models.UserMessagerie;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace Projet2.Controllers
{
    public class AdminController : Controller
    {

        private Dal dal;
        private IWebHostEnvironment _webEnv;
        public AdminController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }

        public IActionResult CreateEmployee()
        {

            AdminViewModel model = new AdminViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateEmployee(AdminViewModel model)
        {

            model.Contact =
                 dal.AddContact(
                     model.Contact.EmailAdress,
                     model.Contact.TelephoneNumber
                     );
            model.Infos =
                 dal.AddInfoPerso(
                     model.Infos.FirstName,
                     model.Infos.LastName,
                     model.Infos.Birthday
                     );

            model.Profile =
                dal.CreateProfileEmployee(
                    model.Profile.Bio,
                    model.Profile.Games
                    );
           model.Messagerie =
              dal.AddMessagerie();
            model.Account =
            dal.AddAccount(
                 model.Account.Username,
                 model.Account.Password,
                 model.Contact.Id,
                 model.Infos.Id,
                 model.Profile.Id,
                 model.Account.role,
                 model.Messagerie.Id
                 );

            model.Employee = dal.CreateEmployee(model.Account.Id, model.Employee.JobName, model.Employee.SerialNumber);

            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.Account.Id.ToString()),
                };
            var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
            HttpContext.SignInAsync(userPrincipal);
            model.Stuffs = dal.GetStuffs();
            List<Stuff> Stuffs = model.Stuffs;
            model.ReservationStuffs = dal.GetReservations();
            List<ReservationStuff> ListReservations = model.ReservationStuffs;
            return RedirectToAction("ViewDashboard", model);

        }

        public IActionResult ViewDashboard()
        {
            AdminViewModel model = new AdminViewModel() { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            Account accountUser = dal.GetAccount(HttpContext.User.Identity.Name);
            model.Account = accountUser;
            if (accountUser != null&& model.Account.role==Role.Admin)
            {
                model.Accounts = dal.GetAccounts();
                List<Account> accounts = model.Accounts;


                var ListAccountsEmployees = GetAllAccountsEmployee();
                model.ListAccountsEmployee = new List<SelectListItem>();
                foreach (var account in ListAccountsEmployees)
                {
                    model.ListAccountsEmployee.Add(new SelectListItem
                    {
                        Text = account.Text,
                        Value = account.Value,
                    });
                };

               var ListAccountsBenevole = GetAllAccountsBenevole();
                model.ListAccountsBenevole = new List<SelectListItem>();
                foreach (var account in ListAccountsBenevole)
                {
                    model.ListAccountsBenevole.Add(new SelectListItem
                    {
                        Text = account.Text,
                        Value = account.Value,
                    });
                };


                var ListAccountsAdherent = GetAllAccountsAdherents();
                model.ListAccountsAdherent = new List<SelectListItem>();
                foreach (var account in ListAccountsAdherent)
                {
                    model.ListAccountsAdherent.Add(new SelectListItem
                    {
                        Text = account.Text,
                        Value = account.Value,
                    });
                };

                model.Stuffs = dal.GetStuffs();
                List<Stuff> Stuffs = model.Stuffs;
  
                List<ReservationStuff> ListReservations = dal.GetReservations();
                model.ReservationStuffs = ListReservations;

                List <Publication>publications= dal.GetPublications();
                model.Publications = publications;

                return View(model);
            }



            return RedirectToAction("Login", "Login");
        }


        [HttpPost]
        public IActionResult DeleteAccountAdherent (string selectedaccount)
        {
            AdminViewModel model = new AdminViewModel() { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            
            
            //var selectedAccount = model.selectedAccount;
            
            int idSelected = int.Parse(selectedaccount);
            ///////////GET
            Adherent aToDelete=dal.GetAdherents().Where(r => r.AccountId == idSelected).FirstOrDefault();
            Account toDelete = dal.GetAccounts().Where(r => r.Id == idSelected).FirstOrDefault();
            Contact contact= dal.GetContacts().Where(r => r.Id == toDelete.ContactId).FirstOrDefault();
            InfoPerso infos=dal.GetInformations().Where(r => r.Id == (int)toDelete.InfoPersoId).FirstOrDefault();
            Profile profile= dal.GetProfiles().Where(r => r.Id == toDelete.ProfileId).FirstOrDefault();
            MessagerieA messagerie =dal.GetMessageries().Where(r => r.Id == toDelete.MessagerieId).FirstOrDefault();
            List <Conversation> conversationS = dal.GetUserConversationsStarter(toDelete.Id);
            List<Conversation> conversationR = dal.GetUserConversationsReplier(toDelete.Id);
            Adhesion adhesion = dal.GetAdhesions().Where(r => r.Id == aToDelete.AdhesionId).FirstOrDefault();
            Contribution contribution= dal.GetContributions().Where(r => r.Id == adhesion.ContributionId).FirstOrDefault();
            Benevole benevole= dal.GetBenevoles().Where(r=>r.Id==aToDelete.BenevoleId).FirstOrDefault();
            IEnumerable<Publication> publications = dal.GetPublications().Where(r => r.AccountId == toDelete.Id);
            Planning planning = dal.GetPlannings().Where(r => r.Id == toDelete.PlanningId).FirstOrDefault();
            List<Stuff> stuffs = dal.GetOwnedStuff(toDelete.Id);
            IEnumerable<Slot> slots = dal.GetSlots().Where(r => r.PlanningId == planning.Id);

            ///////REMOVE
            foreach (var slot in slots)
            {
                dal.RemoveSlot(slot);
            }

            foreach (var stuff in stuffs)
            {
                dal.RemoveStuff(stuff);
            }

            foreach (var publication in publications)
            {
                dal.RemovePublication(publication.Id);
            }

            dal.RemovePlanning(planning);
            dal.RemoveBenevole(benevole);
            dal.RemoveContribution(contribution);
            dal.RemoveAdhesion(adhesion);
            dal.RemoveContact(contact);
            dal.RemoveInfos(infos);
            foreach(var conversation in conversationS)
            {
                dal.RemoveConversation(conversation);
            }
            foreach (var conversation in conversationR)
            {
                dal.RemoveConversation(conversation);
            }

            dal.RemoveMessagerie(messagerie);
            dal.RemoveProfile(profile);
            dal.RemoveAccount(toDelete);
            dal.RemoveAdherent(aToDelete);
            
            return RedirectToAction("ViewDashboard", model);
        }


        [HttpPost]
        public IActionResult DeleteAccountBenevole(string selectedaccount)
        {
            AdminViewModel model = new AdminViewModel() { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            int idSelected = int.Parse(selectedaccount);
            ///////////GET
            Benevole aToDelete = dal.GetBenevoles().Where(r => r.AccountId == idSelected).FirstOrDefault();
            Account toDelete = dal.GetAccounts().Where(r => r.Id == idSelected).FirstOrDefault();
            Contact contact = dal.GetContacts().Where(r => r.Id == toDelete.ContactId).FirstOrDefault();
            InfoPerso infos = dal.GetInformations().Where(r => r.Id == (int)toDelete.InfoPersoId).FirstOrDefault();
            Profile profile = dal.GetProfiles().Where(r => r.Id == toDelete.ProfileId).FirstOrDefault();
            MessagerieA messagerie = dal.GetMessageries().Where(r => r.Id == toDelete.MessagerieId).FirstOrDefault();
            List<Conversation> conversationS = dal.GetUserConversationsStarter(toDelete.Id);
            List<Conversation> conversationR = dal.GetUserConversationsReplier(toDelete.Id);
            IEnumerable<Publication> publications = dal.GetPublications().Where(r => r.AccountId == toDelete.Id);
            Planning planning = dal.GetPlannings().Where(r => r.Id == toDelete.PlanningId).FirstOrDefault();
            List<Stuff> stuffs = dal.GetOwnedStuff(toDelete.Id);
            IEnumerable<Slot> slots = dal.GetSlots().Where(r => r.PlanningId == planning.Id);

            ///////REMOVE
            foreach (var slot in slots)
            {
                dal.RemoveSlot(slot);
            }
            foreach (var stuff in stuffs)
            {
                dal.RemoveStuff(stuff);
            }
            foreach (var publication in publications)
            {
                dal.RemovePublication(publication.Id);
            }
            dal.RemovePlanning(planning);
            dal.RemoveContact(contact);
            dal.RemoveInfos(infos);
            foreach (var conversation in conversationS)
            {
                dal.RemoveConversation(conversation);
            }
            foreach (var conversation in conversationR)
            {
                dal.RemoveConversation(conversation);
            }
            dal.RemoveMessagerie(messagerie);
            dal.RemoveProfile(profile);
            dal.RemoveAccount(toDelete);
            dal.RemoveBenevole(aToDelete);

            return RedirectToAction("ViewDashboard", model);
        }



        [HttpPost]
        public IActionResult DeleteAccountEmployee(string selectedaccount)
        {
            AdminViewModel model = new AdminViewModel() { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            int idSelected = int.Parse(selectedaccount);
            ///////////GET
            Employee aToDelete = dal.GetEmployees().Where(r => r.AccountId == idSelected).FirstOrDefault();
            Account toDelete = dal.GetAccounts().Where(r => r.Id == idSelected).FirstOrDefault();
            Contact contact = dal.GetContacts().Where(r => r.Id == toDelete.ContactId).FirstOrDefault();
            InfoPerso infos = dal.GetInformations().Where(r => r.Id == (int)toDelete.InfoPersoId).FirstOrDefault();
            Profile profile = dal.GetProfiles().Where(r => r.Id == toDelete.ProfileId).FirstOrDefault();
            MessagerieA messagerie = dal.GetMessageries().Where(r => r.Id == toDelete.MessagerieId).FirstOrDefault();
            List<Conversation> conversationS = dal.GetUserConversationsStarter(toDelete.Id);
            List<Conversation> conversationR = dal.GetUserConversationsReplier(toDelete.Id);
            IEnumerable<Publication> publications = dal.GetPublications().Where(r => r.AccountId == toDelete.Id);
            Planning planning = dal.GetPlannings().Where(r => r.Id == toDelete.PlanningId).FirstOrDefault();
            List<Stuff> stuffs = dal.GetOwnedStuff(toDelete.Id);
            IEnumerable<Slot> slots = dal.GetSlots().Where(r => r.PlanningId == planning.Id);
            IEnumerable<Activity> activities = dal.GetActivities().Where(r => r.PublisherId == toDelete.Id);
            ///////REMOVE
            foreach (var slot in slots)
            {
                dal.RemoveSlot(slot);
            }
            foreach (var stuff in stuffs)
            {
                dal.RemoveStuff(stuff);
            }
            foreach (var publication in publications)
            {
                dal.RemovePublication(publication.Id);
            }
            foreach (var activity in activities)
            {
                dal.RemoveActivities(activity);
            }
            dal.RemovePlanning(planning);
            dal.RemoveContact(contact);
            dal.RemoveInfos(infos);
            foreach (var conversation in conversationS)
            {
                dal.RemoveConversation(conversation);
            }
            foreach (var conversation in conversationR)
            {
                dal.RemoveConversation(conversation);
            }
            dal.RemoveMessagerie(messagerie);
            dal.RemoveProfile(profile);
            dal.RemoveAccount(toDelete);
            dal.RemoveEmployee(aToDelete);

            return RedirectToAction("ViewDashboard", model);
        }



        ////////////////////////////////////////////////////////////////
        public IActionResult ProfileViewAdmin()
        {
            AdminViewModel avm = new AdminViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            Account accountUser = dal.GetAccount(HttpContext.User.Identity.Name);
            avm.Account = accountUser;
            if (accountUser != null)
            {

                avm.Profile = dal.GetProfiles().Where(r => r.Id == accountUser.ProfileId).FirstOrDefault();
                avm.Infos = dal.GetInformations().Where(r => r.Id == accountUser.InfoPersoId).FirstOrDefault();
                avm.Contact = dal.GetContacts().Where(r => r.Id == accountUser.ContactId).FirstOrDefault();
                avm.Stuffs = dal.GetOwnedStuff(accountUser.Id);
                List<Stuff> Stuffs = avm.Stuffs;
                avm.ReservationStuffs = dal.GetReservations();
                List<ReservationStuff> ListReservations = avm.ReservationStuffs;

                IEnumerable<Activity> lastactivities = dal.GetActivities();
                avm.Activities = lastactivities.Reverse<Activity>().Take(3);

                IEnumerable<Publication> lastpublications = dal.GetPublications();
                avm.Publications = lastpublications.Reverse<Publication>().Take(3);

                foreach (var Publication in avm.Publications)
                {
                    Account AuthorPubli = dal.GetAccounts().Where(r => r.Id == Publication.AccountId).FirstOrDefault();
                    if (AuthorPubli != null) { Publication.Account = AuthorPubli; }
                }

                return View(avm);
            }

            return RedirectToAction("Login", "Login");

        }

       

      

        public List<SelectListItem> GetAllAccountsAdherents()
        {
            List<SelectListItem> SelectionAccountsAdherents = new List<SelectListItem>();
            var adherents=dal.GetAdherents();
            foreach (Adherent adherent in adherents)
            {
            Account account = adherent.Account;

               
                        SelectionAccountsAdherents.Add(new SelectListItem { Text = account.Username, Value = account.Id.ToString() });
                    
                  
            }
            return SelectionAccountsAdherents;
        }
        public List<SelectListItem> GetAllAccountsBenevole()
        {
            List<SelectListItem> SelectionAccountsBenevole = new List<SelectListItem>();
           var benevoles = dal.GetBenevoles();

            foreach (Benevole benevole in benevoles)
            {
                Account account = benevole.Account;

                var adherents = dal.GetAdherents();
                foreach(var adherent in adherents)
                {
                    if (benevole.AccountId!=adherent.AccountId)
                    {
                        SelectionAccountsBenevole.Add(new SelectListItem { Text = account.Username, Value = account.Id.ToString() });
                    }
                } 
            }
            return SelectionAccountsBenevole;
        }

        public List<SelectListItem> GetAllAccountsEmployee()
        {
            List<SelectListItem> SelectionAccountsEmployee = new List<SelectListItem>();
            var employees = dal.GetEmployees();
            
            foreach (Employee employee in employees)
            {

                Account account = employee.Account;
                SelectionAccountsEmployee.Add(new SelectListItem { Text = account.Username, Value = account.Id.ToString() });
                //var accountsemployees = dal.GetAccounts().Where(r => r.Id == employee.AccountId);

                //foreach (Account account1 in accountsemployees)
                //{
                //    SelectionAccountsEmployee.Add(new SelectListItem { Text = account1.Username, Value = account1.Id.ToString() });
                //}
            }
            return SelectionAccountsEmployee;
        }

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin", "Login");
        }

    }
}
