using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Projet2.Models;
using Projet2.Models.Messagerie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message = Projet2.Models.Messagerie.Message;
using Conversation = Projet2.Models.Messagerie.Conversation;
using Projet2.Models.UserMessagerie;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projet2.ViewModels
{
    public class MessagerieViewModel
    {
        //public PagedList.IPagedList<MessageSent> Messages { get; set; }
        //private List<MessageReply> _replies = new List<MessageReply>();
        //public MessageSent SentMessage { get; set; }
        //public List<MessageSent> SentMessages { get; set; }
        //public MessageReply Reply { get; set; }
        //public List<MessageReply> Replies
        //{
        //    get { return _replies; }
        //    set { _replies = value; }
        

        public Conversation Conversation { get; set; }
        public List <Conversation> UserConversationsStarter { get; set; }
        public List<Conversation> UserConversationsReceiver { get; set; }
        public Message Message { get; set; }
        public List<Message> Messages { get; set; }
        public Account Account { get; set; }
        public List<Account> Accounts { get; set; }
        public MessagerieA Messagerie { get; set; }
        public Profile Profile { get; set; }

        public InfoPerso Info { get; set; }

        public List<SelectListItem> ListAccounts { get; set; }

        public int[] AccountIds { get; set; }

        public string selectedAccount { get; set; }
     
        ////////////////////FIN
    }


}
