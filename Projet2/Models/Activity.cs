using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime EndDate { get; set; }

        public int? SlotID { get; set; }
        public virtual Slot Slot { get; set; }

        //public virtual List<Benevole> ListeBenevoles { get; set; }
    }
}
