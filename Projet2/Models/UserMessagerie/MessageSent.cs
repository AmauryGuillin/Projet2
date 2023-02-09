using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Projet2.Models.Messagerie
{
    public class MessageSent
    {
       
        public int Id { get; set; }
        [MaxLength(30)]
       
        
        public int? SenderId { get; set; }
        public virtual Profile Sender { get; set; }
        public int? ReceiverId { get; set; }
        public virtual Profile Receiver { get; set; }
        public DateTime MessageTimeStamp { get; set; }
        public string Body { get; set; }
        public Boolean isRead { get; set; }

    }
    


}
