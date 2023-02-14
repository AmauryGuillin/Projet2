using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
    /// <summary>
    ///  This class represents a Slot.
    /// It is connected to the Activity class and Planing class.
    /// </summary>
    public class Slot
    {
        /// <summary>
        /// Gets or sets the slot ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets the date on which the slot is set
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime Date { get; set; } //a régler que sur la date

        /// <summary>
        /// Gets or sets the start time at which the slot is set
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime StartHour { get; set; } //a régler que sur l'heure

        /// <summary>
        /// Gets or sets the end time at which the slot is set
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime EndHour { get; set; } //a régler que sur l'heure

        /// <summary>
        /// Gets or sets user activity relative to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the account user activity.
        /// </summary>
        public virtual Activity Activity { get; set; }

        /// <summary>
        /// Gets or sets the user's schedule relative to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? PlanningId { get; set; }

        /// <summary>
        /// Gets or sets the account user's schedule.
        /// </summary>
        public virtual Planning Planning { get; set; }

    }
}
