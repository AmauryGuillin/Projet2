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

        //public int? Messagerie1Id { get; set; }
        //public MessagerieA Messagerie1 { get; set; }

        //public int? Messagerie2Id { get; set; }
        //public MessagerieA Messagerie2 { get; set; }

    }
}
