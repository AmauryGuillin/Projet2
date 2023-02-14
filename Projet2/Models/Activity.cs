using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Projet2.Models
{

    /// <summary>
    /// This class represents an activity. 
    /// Some users (Adherent,Benevole) can register for activities. 
    /// An employee can create and modify an activity. 
    /// An administrator is the only one who can delete an activity.
    /// </summary>
    public class Activity
    {

        /// <summary>
        /// Gets or sets the activity ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the start date of the activity.
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the activity.
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the activity type.
        /// The different types of activities are provided in an enumeration.
        /// </summary>
        public ActivityType activityType { get; set; }

        /// <summary>
        /// Gets or sets the activity description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the location of the activity.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets the organizer of the activity.
        /// </summary>
        public string Organizer { get; set; }

        /// <summary>
        /// Gets or sets the path to the activity image
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets an IFormFile object that contains the activity image (not database-mapped).
        /// </summary>
        [NotMapped]
        public IFormFile ActivityImage { get; set; }

        /// <summary>
        /// Gets or sets the event type of the activity.
        /// The different activity event types are provided in an enumeration.
        /// </summary>
        public EventType ActivityEventType { get; set; }

        /// <summary>
        /// Gets or sets the number of participants in the activity.
        /// </summary>
        public int NumberOfParticipants { get; set; }

        /// <summary>
        /// Gets or sets the topic of the activity.
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created the activity relative to their user account ID.
        /// Serves as a foreign key in the database.
        /// </summary>
        public int? PublisherId { get; set; }

        /// <summary>
        /// Gets or sets the account of the user who created the activity.
        /// </summary>
        public Account Publisher { get; set; }
    }

    /// <summary>
    /// Enumeration that represents the different types of activities.
    /// </summary>
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
    /// <summary>
    /// Enumeration that represents the different event types of the activity.
    /// </summary>
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

    /// <summary>
    /// Enumeration that represents the different levels of the person registering for the activity. 
    /// This level is mainly used for the training activity.
    /// </summary>
    public enum Niveau
    {

        /// <summary>
        /// Maximum power level representing Sauron.
        /// </summary>
        [Display(Name = "Sauron")]
        Sauron,

        /// <summary>
        /// Power level representing Galadriel.
        /// Level just below Sauron.
        /// </summary>
        [Display(Name = "Galadriel")]
        Galadriel,

        /// <summary>
        /// Power level representing Gandalf Le Blanc. 
        /// Average level.
        /// </summary>
        [Display(Name = "Gandalf Le Blanc")]
        Gandalf_Le_Blanc,

        /// <summary>
        /// Power level representing Sam Gamgee The Brave. 
        /// This level is just above orc.
        /// </summary>
        [Display(Name = "Sam Gamgee Le Brave")]
        Sam_Gamgee_Le_Brave,

        /// <summary>
        /// Minimum power level representing an Orc. 
        /// Lowest level.
        /// </summary>
        [Display(Name = "Orc")]
        Orc
    }

}
