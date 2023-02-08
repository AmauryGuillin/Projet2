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





    }

    public enum ActivityType
    {
        Tournoi,
        Evenement,
        Activité_association
    }


}
