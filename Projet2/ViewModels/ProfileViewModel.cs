using Projet2.Models;
using Projet2.Models.Informations;
using System.Collections.Generic;

namespace Projet2.ViewModels
{
    public class ProfileViewModel
    {
        public Account account { get; set; }
        public Contact contact { get; set; }
        public Profile profile { get; set; }
        public InfoPerso infos { get; set; }
        public Inventory inventory { get; set; }
        public Stuff stuff { get; set; }
       
    }
}
