using Microsoft.AspNetCore.Mvc;
using Projet2.Models;
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
    }

}
