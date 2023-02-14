using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Projet2.Models
{


    /// <summary>
    /// This class represents a contribution.
    /// Membership fee is required to become a member.
    /// </summary>
    public class Contribution
    {
        /// <summary>
        /// Gets or sets the contribution ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the status of the contribution payment
        /// </summary>
        //[Required]
        public bool PaymentStatus { get; set; }

        /// <summary>
        /// Obtains or defines the RIB numbers to make the direct debit payment
        /// </summary>
        [Display (Name ="Veuillez entrer votre RIB")]
        public string RIB { get; set; }

        /// <summary>
        /// Gets or sets the debit date for the payment of the contribution.
        ///The different possible direct debits are provided in an enumeration.
        /// </summary>
        [Display(Name = "Veuillez choisir une date de prelèvement parmi celles proposées :")]
        public PrelevementDate PrelevementDate { get; set;}

        /// <summary>
        /// Gets or sets the type of contribution for the payment of the subscription resulting in the choice between an annual, quarterly or monthly levy.
        ///The different types of possible contribution are provided in an enumeration.
        /// </summary>
        [Display(Name = "Veuillez choisir la frequence de votre paiement :")]
        public ContributionType ContributionType { get; set;}
 
    }
    /// <summary>
    /// Enumeration that represents the different payment options.
    /// </summary>
    /// </summary>
    public enum ContributionType
    {
        [Display(Name = "Annuel")]
        Annuel,
        [Display(Name = "Trimestriel")]
        Trimestriel,
        [Display(Name = "Mensuel")]
        Mensuel
    }

    /// <summary>
    /// Enumeration that represents the different picking possibilities for the date.
    /// </summary>
    public enum PrelevementDate
    {
        [Display(Name = "5 du mois")]
        CinqDuMois,
        [Display(Name = "15 du mois")]
        QuinzeDuMoi,
        [Display(Name = "20 du mois")]
        VingtCingDuMois
    }
}