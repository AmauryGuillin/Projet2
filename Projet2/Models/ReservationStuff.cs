using System;

namespace Projet2.Models
{
    public class ReservationStuff
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public int? StuffId { get; set; }
        public virtual Stuff Stuff { get; set; }

    }
}
