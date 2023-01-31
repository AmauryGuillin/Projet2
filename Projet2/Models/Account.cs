
using Projet2.Models.Informations;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
    public class Account
    {

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public int? InfoPersoId { get; set; }
        public virtual InfoPerso infoPerso { get; set; }

        public int? ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        public int? PlanningId { get; set; }
        public virtual Planning Planning { get; set; }

        public int? SportAssociationId { get; set; }
        public virtual SportAssociation SportAssociation { get; set; }

        public int? InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }


        public int? ProfileId { get; set; }
        public virtual Profile Profile { get; set; }



      

       

        ////////////END
    }
}
