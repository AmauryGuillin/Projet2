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
    /// <summary>
    /// Controller for admin.
    /// </summary>
    public class AdminController : Controller
    {

        private Dal dal;//An instance of the "Dal" class

        private IWebHostEnvironment _webEnv;//An instance of the "IWebHostEnvironment" interface

        /// <summary>
        /// Initializes a new instance of the AdminController class.
        /// </summary>
        /// <param name="environment">The application's WebHost environment.</param>
        public AdminController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }

        /// <summary>
        /// Returns a view for creating an employee with the logged in user's information.
        /// </summary>
        /// <returns>An employee creation view if the user is an authenticated administrator, otherwise a redirect to the login page.</returns>
        public IActionResult CreateEmployee()
        {
            AdminViewModel model = new AdminViewModel() { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            Account accountUser = dal.GetAccount(HttpContext.User.Identity.Name);
            model.Account = accountUser;
            if (accountUser != null && model.Account.role == Role.Admin)
            {
                Account newEmployee = new Account();
                newEmployee = model.CreatedEmployee;
                InfoPerso empInfos= new InfoPerso();
                empInfos = model.Infos;
                Contact contact= new Contact();
                contact = model.Contact;
                Employee CreatedEmployee= new Employee();
                CreatedEmployee = model.Employee;

                return View(model);
            }

           return  RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Process the data submitted by the create employee form and add a new employee in the data base.
        /// </summary>
        /// <param name="model">The AdminViewModel containing the data submitted by the form.</param>
        /// <returns>A redirect to the Dashboard view if the employee addition was successful, otherwise a create employee view with validation errors.</returns>
        [HttpPost]
        public IActionResult CreateEmployee(AdminViewModel model)
        {
            Account newEmployee= new Account();
            newEmployee = model.CreatedEmployee;
            Employee employee= new Employee();


            model.CreatedEmployee.Contact =
                 dal.AddContact(
                     model.CreatedEmployee.Contact.EmailAdress,
                     model.CreatedEmployee.Contact.TelephoneNumber
                     );
            model.CreatedEmployee.infoPerso =
                 dal.AddInfoPerso(
                     model.CreatedEmployee.infoPerso.FirstName,
                     model.CreatedEmployee.infoPerso.LastName,
                     model.CreatedEmployee.infoPerso.Birthday
                     );

            model.CreatedEmployee.Profile =
                dal.CreateProfileEmployee(
                    "Lorem ipsus",
                    "Lorem ipsus"
                    );
           model.CreatedEmployee.Messagerie =
              dal.AddMessagerie();
            model.CreatedEmployee =
            dal.AddAccount(
                 model.CreatedEmployee.Username,
                 model.CreatedEmployee.Password,
                 model.CreatedEmployee.Contact.Id,
                 model.CreatedEmployee.infoPerso.Id,
                 model.CreatedEmployee.Profile.Id,
                 Role.Salarie,
                 model.CreatedEmployee.Messagerie.Id
                 );

            model.Employee = dal.CreateEmployee(model.CreatedEmployee.Id, model.Employee.JobName, model.Employee.SerialNumber);

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

        /// <summary>
        /// Displays the admin dashboard with information of accounts, bookings, posts and drop-downs for selecting different account types.
        /// </summary>
        /// <returns>A view containing the admin dashboard information or redirects to the login page if the user is not logged in or is not an admin.</returns>
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

        /// <summary>
        /// HTTP Post method to delete a member account.
        /// </summary>
        /// <param name="selectedaccount">The identifier of the account selected for deletion.</param>
        /// <returns>Redirects to admin dashboard.</returns>
        [HttpPost]
        public IActionResult DeleteAccountAdherent (string selectedaccount)
        {
            AdminViewModel model = new AdminViewModel() { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            
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

        /// <summary>
        /// HTTP Post method to delete a volunteer account.
        /// </summary>
        /// <param name="selectedaccount">The identifier of the account selected for deletion.</param>
        /// <returns>Redirects to admin dashboard.</returns>
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


        /// <summary>
        /// HTTP Post method to delete a emplloyee account.
        /// </summary>
        /// <param name="selectedaccount">The identifier of the account selected for deletion.</param>
        /// <returns>Redirects to admin dashboard.</returns>
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

        /// <summary>
        /// Method to show admin profile view
        /// </summary>
        /// <returns>The admin profile view</returns>
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




        /// <summary>
        /// Retrieve all members accounts as a list of selectable items.
        /// </summary>
        /// <returns>List of members accounts as selectable items</returns>
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

        /// <summary>
        /// Retrieve all volonteers accounts as a list of selectable items.
        /// </summary>
        /// <returns>List of volonteers accounts as selectable items</returns>
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

        /// <summary>
        /// Retrieve all employees accounts as a list of selectable items.
        /// </summary>
        /// <returns>List of employees accounts as selectable items</returns>
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
