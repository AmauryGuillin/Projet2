namespace Projet2.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Theme { get; set; }
        public int NumberOfParticipants { get; set; }
        public EventType EventType { get; set; }
        public int? ActivityId { get; set; }
        public Activity Activity { get; set; }
    }

    //public enum EventType
    //{
    //    Aucun,
    //    Coaching,
    //    Autre,
        
    //}



}
