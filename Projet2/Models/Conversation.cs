using System.Collections.Generic;

namespace Projet2.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
    }
}
