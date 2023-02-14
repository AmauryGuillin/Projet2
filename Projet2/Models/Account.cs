
using Projet2.Models.Informations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Projet2.Models.UserMessagerie;

namespace Projet2.Models
{

    /// <summary>
    /// This class represents a user's account. 
    /// This account is the center of the application. 
    /// The account id allows the operation of the majority of the functionalities and accesses concerning the application.
    /// </summary>
    public class Account
    {

        /// <summary>
        /// Gets or sets the account ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        [Required]
        [Display(Name = "Pseudo: ")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        [Required]
        [Display(Name = "Mot de passe: ")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user's personal information relative to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? InfoPersoId { get; set; }

        /// <summary>
        /// Gets or sets the account user's personal information.
        /// </summary>
        public virtual InfoPerso infoPerso { get; set; }

        /// <summary>
        /// Gets or sets the user's contact information related to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? ContactId { get; set; }

        /// <summary>
        /// Gets or sets the account user's contact information.
        /// </summary>
        public virtual Contact Contact { get; set; }

        /// <summary>
        /// Gets or sets the user's schedule relative to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? PlanningId { get; set; }

        /// <summary>
        /// Gets or sets the account user's schedule.
        /// </summary>
        public virtual Planning Planning { get; set; }        

        /// <summary>
        /// Gets or sets the inventory where the user's stuff is stored relative to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? InventoryId { get; set; }

        /// <summary>
        /// Gets or sets the account user's inventory.
        /// </summary>
        public virtual Inventory Inventory { get; set; }

        /// <summary>
        /// Gets or sets the user's numeric profile relative to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the account user's numeric profile.
        /// </summary>
        public virtual Profile Profile { get; set; }

        /// <summary>
        /// Gets or sets the user's digital messaging relative to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? MessagerieId { get; set; }

        /// <summary>
        /// Gets or sets the account user's digital messaging.
        /// </summary>
        public MessagerieA Messagerie { get; set; }

        public Role role { get; set; }

    }

    /// <summary>
    /// Enumeration that represents a user's role in the application. 
    /// This role entails different access depending on the pages and features.
    /// </summary>
    public enum Role
    {

        /// <summary>
        /// The user has administrator access to the application. 
        /// He has the most complete role regarding the functionality of the application.
        /// </summary>
        [Display(Name = "Administrateur")]
        Admin,

        /// <summary>
        /// The user is an employee. 
        /// He has less important access than the administrator.
        /// He has more important than the members("Adhérents") and the volunteers("Bénévoles").
        /// </summary>
        [Display(Name = "Salarié")]
        Salarie,

        /// <summary>
        /// The user is a volunteer. 
        /// He has the least access to the various features of the application.
        /// </summary>
        [Display(Name = "Bénévole")]
        Benevole,

        /// <summary>
        /// The user is a member. 
        /// He has more access than the volunteer. 
        /// He has less access than the employee.
        /// </summary>
        [Display(Name = "Adhérent")]
        Adherent
    }

}
