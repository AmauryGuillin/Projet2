using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Projet2.Models
{
    /// <summary>
    /// This class represents stuff owned or borrowed by a user.
    /// </summary>
    public class Stuff
    {

        /// <summary>
        /// Gets or sets the stuff identifier needed by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the stuff's name.
        /// </summary>
        [MaxLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path to the stuff image
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets an IFormFile object that contains the stuff image (not database-mapped).
        /// </summary>
        [NotMapped]
        public IFormFile StuffImage { get; set; }

        /// <summary>
        /// Gets or sets the stuff description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of stuff. 
        /// The different types of stuff are provided in an enumeration.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the state of stuff. 
        /// The different states of stuff are provided in an enumeration.
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Gets or sets the reservation status of the stuff, i.e. free, awaiting acceptance, or already reserved. 
        /// The different stuff reservation states are provided in an enumeration
        /// </summary>
        public Reservation Reservation { get; set; }

        /// <summary>
        ///Gets or sets the id of the stuff owner relative to their user account id.
        ///Serves as a foreign key in the database.
        /// </summary>
        public int? AccountOwnerId { get; set; }

        /// <summary>
        /// Gets or sets the stuff owner's account.
        /// </summary>
        public virtual Account AccountOwner { get; set; }

        /// <summary>
        ///Gets or sets the id of the stuff borrower relative to their user account id.
        ///Serves as a foreign key in the database.
        /// </summary>
        public int? AccountBorrowerId { get; set; }

        /// <summary>
        /// Gets or sets the borrower's account.
        /// </summary>
        public virtual Account AccountBorrower { get; set; }

        public bool Authentificate { get; set; }


    }

    /// <summary>
    /// Enumeration that represents the state of the stuff.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// The stuff is acceptable. This is the minimum status level.
        /// </summary>
        [Display(Name = "Accéptable")]
        Acceptable,

        /// <summary>
        /// The stuff is in good condition, i.e. it is better than "acceptable"
        /// </summary>
        [Display(Name = "Bon")]
        Bon,

        /// <summary>
        /// The stuff is in very good condition, i.e. better than "good"
        /// </summary>
        [Display(Name = "Très Bon")]
        TrèsBon,

        /// <summary>
        ///  The stuff is new.. This is the maximum status level.
        /// </summary>
        [Display(Name = "Neuf")]
        Neuf
    }

    /// <summary>
    /// Enumeration that represents the type of the stuff.
    /// </summary>
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

    /// <summary>
    /// Enumeration that represents the state of the reservation of the stuff.
    /// </summary>
    public enum Reservation
    {
        /// <summary>
        /// The stuff is free and is not reserved.
        /// </summary>
        [Display(Name = "Libre")]
        libre,

        /// <summary>
        /// The stuff is awaiting acceptance or refusal of the borrowing request.
        /// </summary>
        [Display(Name = "En Attente")]
        enAttente,

        /// <summary>
        /// The stuff is reserved and therefore no longer available for users to borrow until the object is free again.
        /// </summary>
        [Display(Name = "Réservé")]
        reserve
    }


}
