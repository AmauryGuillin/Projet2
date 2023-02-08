using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
    public class Slot
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; } //a régler que sur la date

        [Column(TypeName = "DateTime")]
        public DateTime StartHour { get; set; } //a régler que sur l'heure

        [Column(TypeName = "DateTime")]
        public DateTime EndHour { get; set; } //a régler que sur l'heure

        public int? ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        public int? PlanningId { get; set; }
        public virtual Planning Planning { get; set; }

    }
}
