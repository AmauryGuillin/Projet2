using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
        public enum State
        {
            Neuf,
            TrèsBon,
            Bon,
            Acceptable
        }

        public enum Type
        {
            Ordinateur,
            Console,
            PeripheriquePC,
            PeripheriqueConsole,
            AccessoireBureau,
            Salle
        }


    public class Stuff
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        //public ?? Image { get; set; }
        // a faire ! 
        public string Description { get; set; }
        public Type Type { get; set; }
        public State State { get; set; }

        public int? ProfileId { get; set; }
        public virtual Profile ProfileOwner { get; set; }

        public int? InventoryId { get; set; }
        public virtual Inventory InventoryBorrower { get; set; }
    }
    
}
