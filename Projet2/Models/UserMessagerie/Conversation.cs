using System.Collections.Generic;
using Projet2.Models.UserMessagerie;
namespace Projet2.Models.Messagerie
{
    public class Conversation
    {
       
        public int Id { get; set; }
       public int? FirstSenderId { get; set; }
        public Account SenderAccount { get; set; }

        public int? ReceiverId { get; set; }
        public Account ReceiverAccount { get; set; }

        //public int? MessageId { get; set; }
        //public Message Message { get; set; }

        public int? MessagerieId { get; set; }
        public MessagerieA Messagerie { get; set; }

    }
}
