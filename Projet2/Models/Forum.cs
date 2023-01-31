using System.Collections.Generic;

namespace Projet2.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public List<Post> Posts { get; set; }
    }
}
