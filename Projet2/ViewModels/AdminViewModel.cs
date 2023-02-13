using Projet2.Models;
using Projet2.Models.Informations;
using Projet2.Models.UserMessagerie;
using System.Collections.Generic;

namespace Projet2.ViewModels
{
    public class AdminViewModel
    {
        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; }
        public Account Account { get; set; }
        public List<Account> Accounts { get; set; }
        public Benevole Benevole { get; set; }
        public List<Benevole>Benevoles { get; set; }
        public Adherent Adherent { get; set; }
        public List <Adherent> Adherents { get; set; }
        public Contact Contact { get; set; }
        public Profile Profile { get; set; }
        public InfoPerso Infos { get; set; }
        public Inventory Inventory { get; set; }
        public MessagerieA Messagerie { get; set; }
        public bool Authentificate { get; set; }

        public List<Stuff> Stuffs { get; set; }
        public ReservationStuff ReservationStuff { get; set; }
        public List<ReservationStuff> ReservationStuffs { get; set; }

        public Activity Activity { get; set; }
        public IEnumerable<Activity> Activities { get; set; }

        public Publication Publication { get; set; }
        public IEnumerable<Publication> Publications { get; set; }

        public Stuff Stuff { get; set; }
       
       


        //public Stuff Stuff { get; set; }
    }

}
