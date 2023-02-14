using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
    /// <summary>
    /// This class represents a member
    /// It is connected to the account class
    /// </summary>
    public class Adherent
    {
        /// <summary>
        /// Gets or sets the adherent ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the volunteer attached to the Adherent.
        /// A member remains a volunteer while the reverse is not true.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? BenevoleId { get; set; }

        /// <summary>
        /// Gets or sets the patron's volunteer object.
        /// </summary>
        public virtual Benevole Benevole { get; set; }

        /// <summary>
        /// Gets or sets the member number
        /// </summary>
        //[Required]
        public long NumAdherent { get; set; }

        /// <summary>
        /// Gets or sets the date of the patron's registration
        /// </summary>
        //[Required]
        public DateTime InscriptionDate { get; set; }

        /// <summary>
        /// Gets or sets the amount of the subscription that the member must pay to join. 
        /// And therefore have access to the features of the member.
        /// </summary>
        public Double Contribution { get; set; }

        /// <summary>
        /// Gets or sets the identity document in PDF format
        /// </summary>
        [Display(Name = " Veuillez Fournir un document d'identité")]
        public string DocPath { get; set; }

        /// <summary>
        /// Gets or sets an IFormFile object that contains the identity document (not mapped to the database).
        /// </summary>
        [NotMapped]
        public IFormFile DocAdherent { get; set; }

        /// <summary>
        /// Gets or sets the Membership ID. 
        /// To join, a follower must have a membership, otherwise the account is a volunteer account.
        /// </summary>
        public int? AdhesionId { get; set; }

        /// <summary>
        /// Gets or sets membership.
        /// </summary>
        public virtual Adhesion Adhesion { get; set; }

        /// <summary>
        /// Gets or sets the subaccount ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        public virtual Account Account { get; set; }

    }
    
}