using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.ViewModels;
using System.Linq;
using Projet2.Models.UserMessagerie;
using Projet2.Models.Messagerie;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
namespace Projet2.Controllers
{
    /// <summary>
    /// Controller for Messagerie.
    /// </summary>
    public class MessagerieController : Controller
    {
        private Dal dal;//An instance of the "Dal" class

        private IWebHostEnvironment _webEnv;//An instance of the "IWebHostEnvironment" interface

        /// <summary>
        /// Initializes a new instance of the MessagerieController class.
        /// </summary>
        /// <param name="environment">The application's WebHost environment.</param>
        public MessagerieController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }

        /// <summary>
        /// Returns the view for the message board.
        /// </summary>
        /// <returns>Returns a view that displays the message board for authenticated users.</returns>
        public IActionResult MessageBoardView()
        {
            MessagerieViewModel mvm = new MessagerieViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (mvm.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                mvm.Account = dal.GetAccount(accountId);
                Account account= mvm.Account;
                mvm.Profile = dal.GetProfile(accountId);
                mvm.Messagerie = dal.GetMessageries().Where(r=>r.Id==account.MessagerieId).FirstOrDefault();
                MessagerieA thisUserMessagerie = mvm.Messagerie;

                mvm.UserConversationsStarter = dal.GetUserConversationsStarter(account.Id);
                List<Conversation> UserStarterConversations = new List<Conversation>();
                UserStarterConversations = mvm.UserConversationsStarter;

                mvm.UserConversationsReceiver= dal.GetUserConversationsReplier(account.Id);
                List<Conversation> UserReceiverConversations = new List<Conversation>();
                UserReceiverConversations=mvm.UserConversationsReceiver;
                 mvm.Messagerie.NbConversations= UserReceiverConversations.Count +UserStarterConversations.Count;
                if (UserStarterConversations.Count != 0)
                {
                    foreach (var conversation in mvm.UserConversationsStarter)
                    {
                        mvm.Conversation= conversation;
                        Account ConvReceiver = new Account();
                        mvm.Conversation.ReceiverAccount = dal.GetAccounts().Where(r=>r.Id==conversation.ReceiverId).FirstOrDefault();
                         ConvReceiver= mvm.Conversation.ReceiverAccount;
                    }
                } 
                if (UserReceiverConversations.Count!=0) 
                {
                    foreach (var conversation in mvm.UserConversationsReceiver)
                    {
                        mvm.Conversation= conversation;
                        Account ConvSender = new Account();
                        mvm.Conversation.SenderAccount = dal.GetAccounts().Where(r => r.Id == conversation.FirstSenderId).FirstOrDefault();
                        ConvSender = mvm.Conversation.SenderAccount;
                    }
                }
                return View(mvm);
            }

                return RedirectToAction("Login","Login");
        }

        /// <summary>
        /// Displays the view for creating a new message conversation.
        /// </summary>
        /// <returns>The view for creating a new message conversation.</returns>
        public IActionResult NewMessageConversationView()
        {
            MessagerieViewModel mvm = new MessagerieViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (mvm.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                mvm.Account = dal.GetAccount(accountId);
                Account Useraccount = mvm.Account;
                mvm.Profile= dal.GetProfile(accountId);
                mvm.Accounts = dal.GetAccounts();
                List<Account> AllAccounts = mvm.Accounts;
                mvm.ListAccounts = GetAllAccounts();
                var AccountData = dal.GetAccounts().Where(r => r.Id != Useraccount.Id);
                mvm.ListAccounts= new List<SelectListItem>();
                foreach(var account in AccountData)
                {
                    mvm.ListAccounts.Add(new SelectListItem
                    {
                        Text = account.Username,
                        Value = account.Id.ToString(),
                    });
                };

                return View(mvm);
            }
                return View("Login","Login");
        }
        /// <summary>
        /// Handles the HTTP POST request for creating a new message conversation.
        /// </summary>
        /// <param name="mvm">The MessagerieViewModel object containing the conversation details.</param>
        /// <returns>The view for the message board.</returns>
        [HttpPost]
        public IActionResult NewMessageConversationView(MessagerieViewModel mvm)
        {
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                mvm.Account = dal.GetAccount(accountId);
                Account accountUser = mvm.Account;
                var selectedAccount = mvm.selectedAccount;
                int idSelected = int.Parse(selectedAccount);
                mvm.Message = dal.FirstMessage(
                    dal.CreateConversation(accountUser.Id, idSelected).Id,
                    accountUser.Id,
                    idSelected,
                    mvm.Message.Body
                    );
                return RedirectToAction("MessageBoardView","Messagerie");
            }
            return View("Login", "Login");
        }



        /// <summary>
        /// Displays the view to reply to a message in a conversation.
        /// </summary>
        /// <param name="id">The ID of the conversation.</param>
        /// <returns>The view to reply to the message.</returns>
        public IActionResult ReplyMessage(int id)
        {
            MessagerieViewModel mvm = new MessagerieViewModel { Authentificate = HttpContext.User.Identity.IsAuthenticated };
            if (mvm.Authentificate == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                mvm.Account = dal.GetAccount(accountId);
                Account Useraccount = mvm.Account;
                mvm.Profile = dal.GetProfile(accountId);
                mvm.Conversation = dal.GetConversations().Where(r => r.Id == id).FirstOrDefault();
                Conversation conversation = mvm.Conversation;
                mvm.Messages = dal.GetMessages().Where(r => r.ConversationId == conversation.Id).ToList();
                var Messages = mvm.Messages;
                List<Message> messages = mvm.Messages;
                foreach(var message in Messages)
                {
                    mvm.Message = message;
                    mvm.Message.Sender = dal.GetAccount((int)mvm.Message.SenderId);
                    Account Sender= mvm.Message.Sender;
                    Profile SenderProfile= dal.GetProfile(Sender.Id);
                    mvm.Message.Sender.Profile= SenderProfile;
                }
                return View(mvm);
            }
            return RedirectToAction("Login", "Login");
        }

        /// <summary>
        /// Handles the HTTP POST request for replying to a message in a conversation
        /// </summary>
        /// <param name="mvm">The MessagerieViewModel containing the reply message</param>
        /// <param name="id">The ID of the conversation being replied to</param>
        /// <returns>A redirect to the MessageBoardView with the updated conversation</returns>
        [HttpPost]
        public IActionResult ReplyMessage(MessagerieViewModel mvm,int id)
        {
            string accountId = (HttpContext.User.Identity.Name);
            mvm.Account = dal.GetAccount(accountId);
            Account Useraccount = mvm.Account;
            mvm.Profile = dal.GetProfile(accountId);

            mvm.Conversation = dal.GetConversations().Where(r => r.Id == id).FirstOrDefault();
            Conversation conversation = mvm.Conversation;
            mvm.Messages = dal.GetMessages().Where(r => r.ConversationId == conversation.Id).ToList();
            int lastSenderId = (int)mvm.Messages.Last().SenderId;
            List<Message> messages = mvm.Messages;
            MessagerieViewModel mvm2= new MessagerieViewModel();
            mvm2.Profile=dal.GetProfile(lastSenderId);
            Message message1 = new Message();
            mvm2.Message = message1;
            message1 = dal.MessageReply(
                conversation.Id,
                Useraccount.Id,
                lastSenderId,
                mvm.Message.Body
                
                );
            mvm2.Message= message1;
            messages.Add(message1);
            return RedirectToAction("MessageBoardView", mvm);
        }

        /// <summary>
        /// Get a list of SelectListItem objects for all registered accounts.
        /// </summary>
        /// <returns>A list of SelectListItem objects for all registered accounts.</returns>
        public List<SelectListItem> GetAllAccounts()
        {
            List<SelectListItem> SelectionAccounts = new List<SelectListItem>();
            foreach (Account account in dal.GetAccounts())
            {
                SelectionAccounts.Add(new SelectListItem{Text= account.Username, Value =account.Id.ToString() }); }
            return SelectionAccounts;
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
