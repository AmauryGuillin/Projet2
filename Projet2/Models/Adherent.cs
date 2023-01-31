using System;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
    public class Adherent
    {
        public int Id { get; set; }
        public int? BenevoleId { get; set; }
        public virtual Benevole Benevole { get; set; }
        [Required]
        public int NumAdherent { get; set; }
        [Required]
        public DateTime InscriptionDate { get; set; }
        public Double Contribution { get; set; }

        public string IDDocuments { get; set; }// ?????
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int? AdhesionId { get; set; }
        public virtual Adhesion Adhesion { get; set; }
        //public int? EntrainementId { get; set; }
        //public Entrainement Entrainement { get; set; }​
    }
    
}