using Projet2.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Projet2.ViewModels
{
    public class ActivitiesViewModel
    {
        public Account Account { get; set; }
        public Slot Slot { get; set; }
        public Planning Planning { get; set; }

        public Activity Activity { get; set; }
        public Event Event { get; set; }

        [Display (Name ="Choisissez le type d'activité")]
        public ActivityType activityType { get; set; }

        [Display(Name = "Choisissez le type d'evenement")]
        public EventType activityEventType { get; set; }

        public List<Activity> activities { get; set;}
    }
}
