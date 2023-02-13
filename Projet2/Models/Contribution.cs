using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Projet2.Models
{
    public enum ContributionType
    {
        [Display(Name = "Annuel")]
        Annuel,
        [Display(Name = "Trimestriel")]
        Trimestriel,
        [Display(Name = "Mensuel")]
        Mensuel
    }

    public enum PrelevementDate
    {
        [Display(Name = "5 du mois")]
        CinqDuMois,
        [Display(Name = "15 du mois")]
        QuinzeDuMoi,
        [Display(Name = "20 du mois")]
        VingtCingDuMois
    }

    public class Contribution
    {
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