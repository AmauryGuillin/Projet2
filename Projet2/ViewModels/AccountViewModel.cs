using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
using Projet2.Models.Informations;
using System.Collections.Generic;

namespace Projet2.ViewModels
{
    public class AccountViewModel 
    {
        public Account Account { get; set; }
        public bool Authentificate { get; set; }
        public Benevole Benevole { get; set; }

        public Adherent Adherent { get; set; }
        public Adhesion Adhesion { get; set; }

        public Contribution Contribution { get; set; }
        public List<ContributionType> contributionTypes { get; set; }
        public Contact contact { get; set; }
        public Profile profile { get; set; }
        public InfoPerso infos { get; set; }
        public Inventory inventory { get; set; }
    }

}
