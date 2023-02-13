using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
        public enum State
        {
        [Display(Name = "Accéptable")]
        Acceptable,
        [Display(Name = "Bon")]
        Bon,
        [Display(Name = "Très Bon")]
        TrèsBon,
        [Display(Name = "Neuf")]
        Neuf
        }

        public enum Type
        {
        [Display(Name = "Ordinateur")]
        Ordinateur,
        [Display(Name = "Console de jeux")]
        Console,
        [Display(Name = "Périphérique Ordinateur")]
        PeripheriquePC,
        [Display(Name = "Périphérique Console de jeux")]
        PeripheriqueConsole,
        [Display(Name = "Accessoire de bureau")]
        AccessoireBureau,
        [Display(Name = "Salle")]
        Salle,
        [Display(Name = "Cosplay")]
        Cosplay,
        [Display(Name = "Autre")]
        Autre
        }

    public enum Reservation
    {
        [Display(Name = "Livre")]
        libre,
        [Display(Name = "En Attente")]
        enAttente,
        [Display(Name = "Réservé")]
        reserve
    }


    public class Stuff
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile StuffImage { get; set; }

        public string Description { get; set; }
        public Type Type { get; set; }
        public State State { get; set; }
        public Reservation Reservation { get; set; }
        public int? AccountOwnerId { get; set; }
        public virtual Account AccountOwner { get; set; }
        public int? AccountBorrowerId { get; set; }
        public virtual Account AccountBorrower { get; set; }
        public bool Authentificate { get; set; }


    }
    
}
