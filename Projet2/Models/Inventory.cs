using System.Collections.Generic;

namespace Projet2.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        public List<Stuff> Stuffs { get; set; }
        
        public int? AccountId { get; set; }
        public virtual Account Account { get; set; }

    }
}
