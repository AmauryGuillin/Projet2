using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Projet2.Models
{
    public class Post
    {
        [MaxLength(10)]
        public int Id { get; set; }
        public string Content { get; set; }
        
        public int ProfilId { get; set; }
        public virtual Profile Author { get; set; }
        public DateTime PostTimeStamp = DateTime.Now;
        public int NbComments { get; set; }
        public List<Comment> Commentaires { get; set; }
    }
}
