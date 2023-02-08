using Projet2.Models;
using System.Collections.Generic;

namespace Projet2.ViewModels
{
    public class PlanningViewModel
    {
        public Account Account { get; set; }
        public Planning Planning { get; set; }
        public Slot Slot { get; set; }

        public List<Slot> slots { get; set; }
        public Profile Profile { get; set; }
        public ActivitiesViewModel Activities { get; set; }

        public List<Activity> activities { get; set; }
    }
}
