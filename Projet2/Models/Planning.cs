using System.Collections.Generic;

namespace Projet2.Models
{
    /// <summary>
    /// This class represents a planning.
    /// It is connected to the account class.
    /// </summary>
    public class Planning
    {
        /// <summary>
        /// Gets or sets the planning ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the planning name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of slots
        /// </summary>
        public int nbSlots { get; set; }
    }
}
