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

        public DateTime StartHour { get; set; } //a régler que sur l'heure

        public DateTime EndHour { get; set; } //a régler que sur l'heure



    }
}
