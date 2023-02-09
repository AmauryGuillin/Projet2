using Projet2.Models.Messagerie;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Projet2.Models.UserMessagerie
{
    public class MessagerieA
    {
        public int Id { get; set; }
        
        public List<Conversation> Conversations { get; set; }

        public int NbConversations { get; set; }


    }
}