using System;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models.Messagerie
{
    public class Message
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public virtual Profile Sender { get; set; }
        public int? ReceiverId { get; set; }
        public virtual Profile Receiver { get; set; }

        public int? ConversationId { get; set; }
        public Conversation UserConversation { get; set; }
        
        public DateTime MessageTimeStamp { get; set; }
        [Display(Name="Veuillez Ecrire votre message")]
        public string Body { get; set; }
        public Boolean isRead { get; set; }
    }
}
