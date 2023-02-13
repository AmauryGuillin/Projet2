using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Projet2.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime EndDate { get; set; }
        public ActivityType activityType { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public string Organizer { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ActivityImage { get; set; }

        public EventType ActivityEventType { get; set; }

        public int NumberOfParticipants { get; set; }
        public string Theme { get; set; }
    }

    public enum ActivityType
    {
        [Display(Name = "Tournoi")]
        Tournoi,
        [Display(Name = "Evènement")]
        Evenement,
        [Display(Name = "Activité de l'association")]
        Activité_association,
        [Display(Name = "Demande de bénévolat")]
        demande_benevolat
    }

    public enum EventType
    {
        [Display(Name = "Aucun")]
        Aucun,
        [Display(Name = "Coaching")]
        Coaching,
        [Display(Name = "Résultat de Compétition")]
        Resultats_competition,
        [Display(Name = "Autre")]
        Autre
    }

    public enum Niveau
    {
        [Display(Name = "Sauron")]
        Sauron,
        [Display(Name = "Galadriel")]
        Galadriel,
        [Display(Name = "Gandalf Le Blanc")]
        Gandalf_Le_Blanc,
        [Display(Name = "Sam Gamgee Le Brave")]
        Sam_Gamgee_Le_Brave,
        [Display(Name = "Orc")]
        Orc
    }

}
