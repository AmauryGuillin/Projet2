using System;

namespace Projet2.Models
{
    public class VolunteeringActivity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? AssociationActivityId { get; set; }
        public AssociationActivity AssociationActivity { get; set; }

    }
}
