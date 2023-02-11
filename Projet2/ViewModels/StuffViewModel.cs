using Projet2.Models.Informations;
using Projet2.Models;
using System.Collections.Generic;
using Projet2.Models.Messagerie;

namespace Projet2.ViewModels
{
    public class StuffViewModel
    {
        public Account Account { get; set; }
        public bool Authentificate { get; set; }
        public Benevole Benevole { get; set; }

        public Adherent Adherent { get; set; }
        public Adhesion Adhesion { get; set; }
        public Contribution Contribution { get; set; }
        public List<ContributionType> contributionTypes { get; set; }
        public Contact Contact { get; set; }
        public Profile Profile { get; set; }
        public InfoPerso Infos { get; set; }
        public Inventory Inventory { get; set; }
        public Stuff Stuff { get; set; }
        public List<Stuff>stuffs { get; set; }
        public ReservationStuff ReservationStuff { get; set; }
        public Message Message { get; set; }
        public Conversation Conversation { get; set; }
        public List<Conversation> UserConversationsStarter { get; set; }
        public List<Conversation> UserConversationsReceiver { get; set; }

        public List<Conversation>AllConversations { get; set; }




        /////////////END

    }
}
