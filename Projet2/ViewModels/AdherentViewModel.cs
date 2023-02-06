using Projet2.Models.Informations;
using Projet2.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Resources;
using System;

namespace Projet2.ViewModels
{
    public class AdherentViewModel
    {
        public Account Account { get; set; }
        public Benevole Benevole { get; set; }
        public Adherent Adherent { get; set; }
        public Adhesion Adhesion { get; set; }
        public Contribution Contribution { get; set; }
        //public ContributionTypes ContributionType { get; set; }
        public Contact Contact { get; set; }
        public Profile Profile { get; set; }
        public InfoPerso Infos { get; set; }
        public Inventory Inventory { get; set; }

        public static IEnumerable<SelectListItem> ContributionTypesList
        {
            get
            {
                foreach (var value in Enum.GetValues(typeof(ContributionType)))
                {
                    string name = string.Format("Annuel", Enum.GetName(typeof(ContributionType), value));
                   
                    yield return new SelectListItem() { Value = value.ToString()};
                }
            }
        }


    }

    public enum ContributionTypes
    {
        Annuel,
        Trimestriel,
        Mensuel
    }

    
}
