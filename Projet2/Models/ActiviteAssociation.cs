using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
    public class ActiviteAssociation : Activity
    {

        public int? ActivityId { get; set; }
        public Activity Activity { get; set; }

        public string Description { get; set; }

        [MaxLength(150)]
        public string Place { get; set; }
    }
}
