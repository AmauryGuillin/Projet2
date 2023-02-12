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
    public class StuffController : Controller
    {
        private Dal dal;
        private IWebHostEnvironment _webEnv;
        public StuffController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }

        public IActionResult CreateStuff()
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            string accountId = (HttpContext.User.Identity.Name);
            model.Account = dal.GetAccount(accountId);
            return View(model);
        }

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
                //dal.CreateStuff(
                //    model.Name, "/images/" + model.StuffImage.FileName, model.Type, model.State);
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
                stuffCreated= model.Stuff;
                //dal.EditStuffCreate(stuffCreated.Id, userAccount.Id, "/images/" + model.Stuff.StuffImage.FileName);
                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }    
            
            return RedirectToAction("Login","Login");
        }

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

        [HttpPost]
        public IActionResult EditStuff(StuffViewModel model, int id)
        {

            if (model.Authentificate == true)
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
            }
            return RedirectToAction("Login", "Login");
        }

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
            }

            return RedirectToAction("Login", "Login");
        }

        public IActionResult StuffCatalog()
        {
            
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            
            if (model.Authentificate == true) {
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
                model.Stuff.AccountBorrowerId = userAccount.Id;
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

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
                            Conversation Conversation= dal.CreateConversation(userAccount.Id, StuffOwner.Id);
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
                             messageauto );
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

        public IActionResult AcceptationBookStuff(int id)
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {

                string accountId = (HttpContext.User.Identity.Name);
                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;
                model.ReservationStuff = dal.GetReservations().Where(r => r.StuffId == stuff.Id).FirstOrDefault();

                model.Stuff.AccountBorrower = dal.GetAccounts().Where(r => r.Id == stuff.AccountBorrowerId).FirstOrDefault();
                
                //model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountBorrowerId).FirstOrDefault();

                //model.Account = dal.GetAccount(accountId);
                //Account userAccount = model.Account;
                //model.Stuff.AccountBorrowerId = userAccount.Id;
                //model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();
            return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult AcceptationBookStuff(StuffViewModel model, int id)
        {
            if (model.Authentificate==true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                model.Account = dal.GetAccount(accountId);
                dal.EditStuffAcceptation(id);
                if (model.Account.role == Role.Adherent)
                {
                    return RedirectToAction("ProfileViewAdherent", "Inscription");
                }
                else if (model.Account.role == Role.Benevole)
                {
                    return RedirectToAction("ProfileViewBenevole", "Inscription");
                }
            }
            return RedirectToAction("Login", "Login");
        }

        public IActionResult CancelationBookStuff(int id)
        {
            StuffViewModel model = new StuffViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (model.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);

                model.Stuff = dal.GetOneStuff(id);
                Stuff stuff = model.Stuff;

                model.ReservationStuff = dal.GetReservations().Where(r => r.StuffId == stuff.Id).FirstOrDefault();
                model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountBorrowerId).FirstOrDefault();

                //model.Account = dal.GetAccount(accountId);
                //Account userAccount = model.Account;
                //model.Stuff.AccountBorrowerId = userAccount.Id;
                //model.Account = dal.GetAccounts().Where(r => r.Id == stuff.AccountOwnerId).FirstOrDefault();
                //dal.EditStuffCancelation(id);

                //int borrowerId = (int)model.Stuff.AccountBorrowerId;
                //Account borrowerAccount = dal.GetAccount(borrowerId);
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }


        [HttpPost]
        public IActionResult CancelationBookStuff(StuffViewModel model, int id)
        {
            
            if (model.Authentificate == true)
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
            }
            return RedirectToAction("Login", "Login");
        }


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
            }
            return RedirectToAction("Login", "Login");
        }



        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("LOgin", "Login");
        }

        ////////////////////RND    

    }
}
