using System;

namespace Projet2.Models
{
    public class ReservationStuff
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public String ReservationArgument { get; set; }
        public Boolean ReservationBorrower { get; set; }
        public Boolean AcceptationOwner { get; set; }
        public int? StuffId { get; set; }
        public virtual Stuff Stuff { get; set; }
        public ReservationStuff() 
        {
            ReservationBorrower= true;
        }

    }
}
