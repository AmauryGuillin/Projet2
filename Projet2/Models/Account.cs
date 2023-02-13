﻿
using Projet2.Models.Informations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Projet2.Models.UserMessagerie;

namespace Projet2.Models
{

    public enum Role
    {
        [Display(Name = "Administrateur")]
        Admin,
        [Display(Name = "Salarié")]
        Salarie,
        [Display(Name = "Bénévole")]
        Benevole,
        [Display(Name = "Adhérent")]
        Adherent
    }
    public class Account
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Pseudo: ")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Mot de passe: ")]
        public string Password { get; set; }

        public int? InfoPersoId { get; set; }
        public virtual InfoPerso infoPerso { get; set; }

        public int? ContactId { get; set; }
        public virtual Contact Contact { get; set; }

        public int? PlanningId { get; set; }
        public virtual Planning Planning { get; set; }

        public int? SportAssociationId { get; set; }
        public virtual SportAssociation SportAssociation { get; set; }

        //public List<Stuff> Stuff { get; set; }
        public int? InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }

        public int? ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        public int? MessagerieId { get; set; }
        public MessagerieA Messagerie { get; set; }

        public Role role { get; set; }



      

       

        ////////////END


    }
}
