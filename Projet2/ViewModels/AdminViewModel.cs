using Projet2.Models;
using Projet2.Models.Informations;
using System.Collections.Generic;

namespace Projet2.ViewModels
{
    public class AdminViewModel
    {
        public Employee Employee { get; set; }
        public Account Account { get; set; }
        public Contact Contact { get; set; }
        public Profile Profile { get; set; }
        public InfoPerso Infos { get; set; }
        public Inventory Inventory { get; set; }
        //public Stuff Stuff { get; set; }
    }
}
