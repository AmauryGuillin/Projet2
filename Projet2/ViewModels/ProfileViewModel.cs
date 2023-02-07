using Projet2.Models;
using Projet2.Models.Informations;
using System.Collections.Generic;

namespace Projet2.ViewModels
{
    public class ProfileViewModel
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

    }
}
