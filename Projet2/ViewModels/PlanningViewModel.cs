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
    }
}
