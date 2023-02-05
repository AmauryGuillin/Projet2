namespace Projet2.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Theme { get; set; }
        public int NumberOfParticipants { get; set; }

        public int? AssociationActivityId { get; set; }
        public AssociationActivity AssociationActivity { get; set; }
    }
}
