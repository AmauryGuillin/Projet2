using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet2.Models;
using Projet2.Models.Messagerie;
using Projet2.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Xml.Linq;

namespace Projet2.Controllers
{
    /// <summary>
    /// Controller for Stuff.
    /// </summary>
    public class StuffController : Controller
    {
        private Dal dal;//An instance of the "Dal" class

        private IWebHostEnvironment _webEnv;//An instance of the "IWebHostEnvironment" interface

        /// <summary>
        /// Initializes a new instance of the StuffController class.
        /// </summary>
        /// <param name="environment">The application's WebHost environment.</param>
        public StuffController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }

        /// <summary>
        /// Create a view to display a form to create a Stuff object.
        /// </summary>
        /// <returns>An IActionResult representing the view</returns>
        public IActionResult CreateStuff()
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            string accountId = (HttpContext.User.Identity.Name);
            model.Account = dal.GetAccount(accountId);
            return View(model);
        }

        /// <summary>
        /// Process an HTTP POST request to create a new Stuff object.
        /// </summary>
        /// <param name="model">The StuffViewModel containing the information about the new Stuff object.</param>
        /// <returns>The ID of the stuff item whose reservation is being cancelled</returns>
        [HttpPost]
        public IActionResult CreateStuff(StuffViewModel model)
        {
            string accountId = (HttpContext.User.Identity.Name);
            string uploads = Path.Combine(_webEnv.WebRootPath, "images");
            string filePath = Path.Combine(uploads, model.Stuff.StuffImage.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Stuff.StuffImage.CopyTo(fileStream);
            }
            
            model.Account = dal.GetAccount(accountId);
            Account userAccount = model.Account;
            model.Stuff = dal.CreateStuff(
                "/images/" + model.Stuff.StuffImage.FileName,
                userAccount.Id,
                model.Stuff.Name,
                model.Stuff.Description,
                model.Stuff.Type,
                model.Stuff.State
                );
            Stuff stuffCreated = new Stuff();
            stuffCreated = model.Stuff;

            if (model.Account.role == Role.Adherent)
            {
                return RedirectToAction("ProfileViewAdherent", "Inscription");
            }
            else if (model.Account.role == Role.Benevole)
            {
                return RedirectToAction("ProfileViewBenevole", "Inscription");
            }
            else if (model.Account.role == Role.Salarie)
            {
                return RedirectToAction("ProfileViewEmployee", "Employee");
            }
            else if (model.Account.role == Role.Admin)
            {
                return RedirectToAction("ProfileViewAdmin", "Admin");
            }

            return RedirectToAction("Login", "Login");
        }
        /// <summary>
        /// Process an HTTP GET request to display the edit form of an existing Stuff object.
        /// </summary>
        /// <param name="id">The identifier of the Stuff object to modify.</param>
        /// <returns>Stuff object edit view.</returns>
        public IActionResult EditStuff(int id)
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                model.Stuff = dal.GetOneStuff(id);
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Process an HTTP POST request to modify the information of an existing Stuff object.
        /// </summary>
        /// <param name="model">A StuffViewModel object containing the updated Stuff object information.</param>
        /// <param name="id">The identifier of the Stuff object to modify.</param>
        /// <returns>The ID of the stuff item whose reservation is being cancelled</returns>
        [HttpPost]
        public IActionResult EditStuff(StuffViewModel model, int id)
        {

            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);

                model.Account = dal.GetAccount(accountId);

                dal.EditStuff(id, model.Stuff.Name, model.Stuff.Description, model.Stuff.Type, model.Stuff.State);

                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }
                else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }
                else if (model.Account.role == Role.Salarie)
                {
                    return RedirectToAction("ProfileViewEmployee", "Employee");
                }
                else if (model.Account.role == Role.Admin)
                {
                    return RedirectToAction("ProfileViewAdmin", "Admin");
                }
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Displays the confirmation page for removing a stuff item
        /// </summary>
        /// <param name="id">The id of the stuff item to be removed</param>
        /// <returns>The view to confirm the removal of the stuff item</returns>
        public IActionResult RemoveStuff(int id)
        {
            ProfileViewModel model = new ProfileViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                model.Stuff = dal.GetOneStuff(id);
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Removes a stuff item from the database
        /// </summary>
        /// <param name="model">The view model containing the account and stuff item</param>
        /// <param name="id">The id of the stuff item to be removed</param>
        /// <returns>Returns the PublicationWall view with a list of all the available stuff.</returns>
        [HttpPost]
        public IActionResult RemoveStuff(ProfileViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                dal.RemoveStuff(id);
                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }
                else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }
                else if (model.Account.role == Role.Salarie)
                {
                    return RedirectToAction("ProfileViewEmployee", "Employee");
                }
                else if (model.Account.role == Role.Admin)
                {
                    return RedirectToAction("ProfileViewAdmin", "Admin");
                }
            }

            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Displays a catalog of available stuff to users that are authenticated.
        /// </summary>
        /// <returns>Returns the StuffCatalog view with a list of all the available stuff.</returns>
        public IActionResult StuffCatalog()
        {

            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };

            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                if (model.Account != null)
                {
                    Account account = model.Account;
                    model.stuffs = dal.GetStuffs();
                    List<Stuff> listStuff = model.stuffs.ToList();
                    return View(model);
                }
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }
        /// <summary>
        /// Display the view to create a stuff reservation and thus be able to borrow a stuff
        /// </summary>
        /// <param name="id">ID of the stuff to create the reservation.</param>
        /// <returns>The view to create the booking request.</returns>
        public IActionResult CreateBookStuff(int id)
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;
                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;
                if (userAccount.role != Projet2.Models.Role.Benevole)
                { 
                model.Stuff.AccountOwner = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();
                model.Stuff.AccountBorrowerId = userAccount.Id;
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();
                return View(model);
                }
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Process an HTTP POST request to create a new stuff reservation.
        /// </summary>
        /// <param name="model">The StuffViewModel containing the information about the new reservation.</param>
        /// <param name="id">The id of the stuff to modify it following the reservation request.</param></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateBookStuff(StuffViewModel model, int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = HttpContext.User.Identity.Name;
                model.ReservationStuff.StuffId = model.Stuff.Id;
                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;
                Account StuffOwner = dal.GetAccount((int)stuff.AccountOwnerId);
                model.Account = dal.GetAccount(accountId);
                Account userAccount = model.Account;
                //Sert à modifier l'accountBorowerId
                dal.EditStuffReservation(model.Stuff.Id, model.Account.Id);
                ReservationStuff reservationCreated = dal.CreateReservationStuff(model.ReservationStuff);
                model.AllConversations = dal.GetConversations();
                var conversations = model.AllConversations;
                string messageauto = "Vous avez une demande de reservation de materiel en attente.Vous pouvez aller la consulter a tout moment";


                if (conversations != null)
                {
                    foreach (var conversation in conversations)
                    {
                        if (conversation.FirstSenderId == (int)StuffOwner.Id && conversation.ReceiverId == userAccount.Id)
                        {
                            Conversation Conversation = dal.CreateConversation(userAccount.Id, StuffOwner.Id);
                            model.Conversation = Conversation;
                            Message message1 = dal.MessageReply(
                            Conversation.Id,
                            userAccount.Id,
                            (int)StuffOwner.Id,
                            messageauto);
                            model.Message = message1;
                            return RedirectToAction("StuffCatalog", model);
                        }
                        else if (conversation.ReceiverId == (int)StuffOwner.Id && conversation.FirstSenderId == userAccount.Id)
                        {
                            Conversation Conversation = dal.CreateConversation(userAccount.Id, StuffOwner.Id);
                            model.Conversation = Conversation;
                            Message message1 = dal.MessageReply(
                            Conversation.Id,
                            userAccount.Id,
                            (int)StuffOwner.Id,
                            messageauto);
                            model.Message = message1;
                            return RedirectToAction("StuffCatalog", model);
                        }
                        else
                        {
                            model.Conversation = dal.CreateConversation(userAccount.Id, StuffOwner.Id);
                            Conversation Conversation = model.Conversation;
                            Message message1 = dal.FirstMessage(
                            Conversation.Id,
                            userAccount.Id,
                            (int)StuffOwner.Id,
                             messageauto);
                            model.Message = message1;
                            return RedirectToAction("StuffCatalog", model);
                        }

                    }
                }

                    Conversation newConversation = dal.CreateConversation(userAccount.Id, StuffOwner.Id);
                    model.Conversation= newConversation;
                    Message message= dal.FirstMessage(
                    newConversation.Id,
                    userAccount.Id,
                    (int)StuffOwner.Id,
                    messageauto);
                    model.Message= message;
                    return RedirectToAction("StuffCatalog", model);

            }
            return RedirectToAction("Login", "Login");

        }
        /// <summary>
        /// Display the view to accept a reservation for stuff
        /// </summary>
        /// <param name="id">ID of reserved stuff.</param>
        /// <return>The view to accept the reservation.</return>
        public IActionResult AcceptationBookStuff(int id)
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;
                model.ReservationStuff = dal.GetReservations().Where(r => r.StuffId == stuff.Id).FirstOrDefault();


                model.Stuff.AccountOwner = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();
                model.Stuff.AccountBorrower = dal.GetAccounts().Where(r => r.Id == stuff.AccountBorrowerId).FirstOrDefault();

                
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Action to accept a material reservation.
        /// </summary>
        /// <param name="model">The view model containing the reservation data.</param>
        /// <param name="id">The id of the stuff in question.</param>
        /// <returns>A redirection to the user's profile page</returns>
        [HttpPost]
        public IActionResult AcceptationBookStuff(StuffViewModel model, int id)
        {
            Account account = dal.GetAccount(HttpContext.User.Identity.Name);
            model.Account = account;

            if (account!=null)

            {
                dal.EditStuffAcceptation(id);
                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }
                else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }
                else if (model.Account.role == Role.Salarie)
                {
                    return RedirectToAction("ProfileViewEmployee", "Employee");
                }
                else if (model.Account.role == Role.Admin)
                {
                    return RedirectToAction("ProfileViewAdmin", "Admin");
                }
            }
            return RedirectToAction("Login", "Login");
        }

        //inutile ????
        public IActionResult CancelationBookStuff(int id)
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {

                string accountId = (HttpContext.User.Identity.Name);

                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;

                model.ReservationStuff = dal.GetReservations().Where(r => r.StuffId == stuff.Id).FirstOrDefault();
                model.Stuff.AccountBorrower = dal.GetAccounts().Where(r => r.Id == stuff.AccountBorrowerId).FirstOrDefault();

                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Action method for cancelling a stuff reservation
        /// </summary>
        /// <param name="model">Stuff view model</param>
        /// <param name="id">ID of the stuff</param>
        /// <returns>A redirection to the user's profile page</returns>
        [HttpPost]
        public IActionResult CancelationBookStuff(StuffViewModel model, int id)
        {

            Account account = dal.GetAccount(HttpContext.User.Identity.Name);
            model.Account = account;
            if (account != null)
            {

                dal.EditStuffCancelation(id);
                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }
                else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }
                else if (model.Account.role == Role.Salarie)
                {
                    return RedirectToAction("ProfileViewEmployee", "Employee");
                }
                else if (model.Account.role == Role.Admin)
                {
                    return RedirectToAction("ProfileViewAdmin", "Admin");
                }
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Action method for displaying the details of a borrowed stuff.
        /// </summary>
        /// <param name="id">The id of the stuff to display.</param>
        /// <returns>The view displaying the details of the borrowed stuff.</returns>
        public IActionResult ConsultationBookStuff(int id)
        {
            ProfileViewModel model = new ProfileViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;
                model.ReservationStuff = dal.GetReservations().Where(r => r.StuffId == stuff.Id).FirstOrDefault();
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountBorrowerId).FirstOrDefault();
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Handles POST requests for cancelling the reservation of a stuff item, and redirects the user to their profile page
        /// </summary>
        /// <param name="model">The ProfileViewModel containing information about the user's profile</param>
        /// <param name="id">The ID of the stuff item whose reservation is being cancelled</param>
        /// <returns>A redirection to the user's profile page</returns>
        [HttpPost]
        public IActionResult ConsultationBookStuff(ProfileViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                dal.EditStuffCancelation(id);
                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }
                else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }
                else if (model.Account.role == Role.Salarie)
                {
                    return RedirectToAction("ProfileViewEmployee", "Employee");
                }
                else if (model.Account.role == Role.Admin)
                {
                    return RedirectToAction("ProfileViewAdmin", "Admin");
                }
            }
            return RedirectToAction("Login", "Login");
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
