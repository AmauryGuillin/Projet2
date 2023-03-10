using Projet2.Models.Informations;
using Projet2.Models;
using System.Collections.Generic;
using Projet2.Models.UserMessagerie;

namespace Projet2.ViewModels
{

    public enum ContributionType
    {
        Annuel,
        Trimestriel,
        Mensuel
    }

    public enum PrelevementDate
    {
        CinqDuMois,
        QuinzeDuMoi,
        VingtCingDuMois
    }
    public class InscriptionViewModel
    {
        public Account Account { get; set; }
        public bool Authentificate { get; set; }
        public Benevole Benevole { get; set; }

        public Adherent Adherent { get; set; }
        public Adhesion Adhesion { get; set; }

        public Contribution Contribution { get; set; }
      
        public Contact Contact { get; set; }
        public Profile Profile { get; set; }
        public InfoPerso Infos { get; set; }
        public Inventory Inventory { get; set; }

        public PrelevementDate PrelevementDate { get; set;}
        public ContributionType ContributionType { get; set; }
        public Stuff Stuff { get; set; }
        public List<Stuff> Stuffs { get; set; }
        public ReservationStuff ReservationStuff { get; set; }
        public List<ReservationStuff> ReservationStuffs { get; set; }

        public MessagerieA Messagerie { get; set; }

        public Activity Activity { get; set; }
        public IEnumerable <Activity> Activities { get; set;}

        public Publication Publication { get; set; }
        public IEnumerable<Publication> Publications { get; set; }


    }
}
