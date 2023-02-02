using System;

namespace Projet2.Models
{

    public enum PublicationTypes
    {
        ArticlesInformatifs,
        Video,
        Infographie,
        Podcast,
        Newsletter,
        FAQ
    }

    public class Publication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PublicationTypes PublicationType { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
