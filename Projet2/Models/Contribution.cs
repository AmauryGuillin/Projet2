using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Projet2.Models
{
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
        

        //[Required]
        public bool PaymentStatus { get; set; }
        //public double TotalCount { get; set; }

        [Display (Name ="Veuillez entrer votre RIB")]
        public string RIB { get; set; }
        [Display(Name = "Veuillez choisir une date de prelèvement parmi celles proposées :")]
        public PrelevementDate PrelevementDate { get; set;}

        [Display(Name = "Veuillez choisir la frequence de votre paiement :")]
        public ContributionType ContributionType { get; set;}
        
        
    }
}