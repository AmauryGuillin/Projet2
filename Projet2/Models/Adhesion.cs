using System;

namespace Projet2.Models
{
    public enum AdhesionStatus
    {
        Verfie,
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