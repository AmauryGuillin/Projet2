using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
    public class Adherent
    {
        public int Id { get; set; }
        public int? BenevoleId { get; set; }
        public virtual Benevole Benevole { get; set; }
        //[Required]
        public int NumAdherent { get; set; }
        //[Required]
        public DateTime InscriptionDate { get; set; }
        public Double Contribution { get; set; }
        [Display(Name = " Veuillez Fournir un document d'identité")]
        public string DocPath { get; set; }// ?????
        [NotMapped]
        public IFormFile DocAdherent { get; set; }

        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int? AdhesionId { get; set; }
        public virtual Adhesion Adhesion { get; set; }

        //public int? CoachingId { get; set; }
        //public virtual Coaching Coaching { get;set; }

        public int? AccountId { get; set; }
        public virtual Account Account { get; set; }

    }
    
}