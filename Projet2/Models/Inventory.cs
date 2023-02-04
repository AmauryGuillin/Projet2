using System.Collections.Generic;

namespace Projet2.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        public List<Stuff> Stuffs { get; set; }

        //rajouter dans compte inventaire en Fk
        public int nbStuff { get; set; }


        public static Inventory CreateInventory()
        {
            Inventory inventory = new Inventory { };

            return inventory;
        }
    }
}
