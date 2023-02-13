using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Projet2.Models
{
    public enum AdhesionStatus
    {
        [Display(Name = "Vérifié")]
        Verfie,
        [Display(Name = "En cours")]
        EnCours
    }

    public class Adhesion
    {
        public int Id { get; set; }

        public int? ContributionId { get; set; }
        public virtual Contribution Contribution { get; set; }

        public DateTime Echeance { get; set; }

        public AdhesionStatus AdhesionStatus { get; set; }

       




    }
}