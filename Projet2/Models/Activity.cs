using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        Tournoi,
        Evenement,
        Activité_association,
        demande_benevolat
    }

    public enum EventType
    {
        Aucun,
        Coaching,
        Resultats_competition,
        Autre
    }

    public enum Niveau
    {
        Sauron,
        Galadriel,
        Gandalf_Le_Blanc,
        Sam_Gamgee_Le_Brave,
        Orc
    }

}
