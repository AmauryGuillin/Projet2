using System.Collections.Generic;

namespace Projet2.Models
{

    /// <summary>
    /// This class represents an inventory
    /// It is connected to the account class
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Gets or sets the inventory ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the list of stuff
        /// </summary>
        public List<Stuff> Stuffs { get; set; }

        /// <summary>
        /// Gets or sets the number of stuff.
        /// </summary>
        public int nbStuff { get; set; }

        /// <summary>
        /// This method creates an inventory
        /// </summary>
        /// <returns>Returns the created inventory</returns>
        public static Inventory CreateInventory()
        {
            Inventory inventory = new Inventory { };

            return inventory;
        }
    }
}
