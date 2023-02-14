using System;

namespace Projet2.Models
{
    /// <summary>
    /// This class represents a reservation for a stuff.
    /// 
    /// </summary>
    public class ReservationStuff
    {
        /// <summary>
        /// Gets or sets the reservation identifier needed by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the arguments advanced by the borrower to request the loan of the stuff.
        /// </summary>
        public String ReservationArgument { get; set; }

        /// <summary>
        /// Gets or sets the ID of stuff the user wants to check out.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? StuffId { get; set; }

        /// <summary>
        /// Gets or sets the stuff the user wants to check out
        /// </summary>
        public virtual Stuff Stuff { get; set; }


    }
}
