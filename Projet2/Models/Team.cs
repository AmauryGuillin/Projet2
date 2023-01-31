using System;

namespace Projet2.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? GameId { get; set; }
        public virtual Games Game { get; set; }
        public DateTime CreationDate { get; set; }
        public int NbAdherent { get; set; }
    }
}