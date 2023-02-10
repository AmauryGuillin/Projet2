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

namespace Projet2.Controllers
{
    public class MessagerieController : Controller
    {
        private Dal dal;
        private IWebHostEnvironment _webEnv;
        public MessagerieController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
            this.dal = new Dal();
        }
        public IActionResult MessageBoardView()
        {
            MessagerieViewModel mvm= new MessagerieViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                mvm.Account = dal.GetAccount(accountId);
                Account account= mvm.Account;
                mvm.Messagerie = dal.GetMessageries().Where(r=>r.Id==account.MessagerieId).FirstOrDefault();
                MessagerieA thisUserMessagerie = mvm.Messagerie;
                mvm.UserConversationsStarter = dal.GetUserConversationsStarter(account.Id);
               
                List<Conversation> UserStarterConversations = new List<Conversation>();
                UserStarterConversations = mvm.UserConversationsStarter;

                mvm.UserConversationsReceiver= dal.GetUserConversationsReplier(account.Id);
                List<Conversation> UserReceiverConversations = mvm.UserConversationsReceiver;

                return View(mvm);
            }

                return RedirectToAction("Login","Login");
        }


        public IActionResult NewMessageConversationView()
        {
            MessagerieViewModel mvm = new MessagerieViewModel();
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                string accountId = (HttpContext.User.Identity.Name);
                mvm.Account = dal.GetAccount(accountId);
                Account Useraccount = mvm.Account;
                mvm.Profile= dal.GetProfile(accountId);
                mvm.Accounts = dal.GetAccounts();
                List<Account> AllAccounts = mvm.Accounts;
                mvm.ListAccounts = GetAllAccounts();

                var AccountData = dal.GetAccounts();
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


        public IActionResult ReplyMessage()
        {
            MessagerieViewModel mvm = new MessagerieViewModel();
            return View(mvm);
        }


        public List<SelectListItem> GetAllAccounts()
        {
            List<SelectListItem> SelectionAccounts = new List<SelectListItem>();
            foreach (Account account in dal.GetAccounts())
            {
                SelectionAccounts.Add(new SelectListItem{Text= account.Username, Value =account.Id.ToString() }); }

            return SelectionAccounts;
        }


        private SelectList GetSelectedAccount(Account selectedAccount)
        {
            var selectedAccounts = dal.GetAccounts();


            return new SelectList(selectedAccounts,selectedAccount);
        }


///////////////////////////END//////////
    }
}
