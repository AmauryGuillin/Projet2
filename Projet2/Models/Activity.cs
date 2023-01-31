using System;
using System.Collections.Generic;

namespace Projet2.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? SlotID { get; set; }
        public virtual Slot Slot { get; set; }

        //public virtual List<Benevole> ListeBenevoles { get; set; }
    }
}
