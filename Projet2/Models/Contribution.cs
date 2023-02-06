using System.ComponentModel.DataAnnotations;

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
        
        [Required]
        public bool PaymentStatus { get; set; }
        //public double TotalCount { get; set; }

        public PrelevementDate PrelevementDate { get; set;}
        public ContributionType ContributionType { get; set;}
        
        
    }
}