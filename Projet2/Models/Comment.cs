using System;

namespace Projet2.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        public int? ProfilId { get; set; }
        public virtual Profile writer { get; set; }

        public DateTime TimeStamp = DateTime.Now;
        public string Content { get; set; }
    }
}
