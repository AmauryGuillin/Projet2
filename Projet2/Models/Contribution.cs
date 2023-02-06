using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Projet2.Models
{
    public enum ContributionType
    {
        Annuel,
        Trimestriel,
        Mensuel
    }

    public enum PrelevementDate
    {
        CinqDuMois,
        QuinzeDuMoi,
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