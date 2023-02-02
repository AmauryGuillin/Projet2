using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
    public class ActiviteAssociation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }

        public int? ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
