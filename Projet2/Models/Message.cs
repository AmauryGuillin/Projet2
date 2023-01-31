using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Projet2.Models
{
    public class Message
    {
        [MaxLength(10)]
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Title { get; set; }
        public int? SenderId { get; set; }
        public virtual Profile Sender { get; set; }
        public int? ReceiverId { get; set; }
        public virtual Profile Receiver { get; set; }
        public DateTime MessageTimeStamp = DateTime.Now;
        public string Body { get; set; }

    }
}
