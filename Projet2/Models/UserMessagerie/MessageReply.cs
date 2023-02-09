using System.ComponentModel.DataAnnotations;
using System;

namespace Projet2.Models.Messagerie
{
    public class MessageReply
    {
       
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string ReplyFrom { get; set; }
        //[Required]
        public string ReplyMessage { get; set; }
        public DateTime ReplyDateTime { get; set; }

        public string Body { get; set; }
        public Boolean isRead { get; set; }
    }
}
