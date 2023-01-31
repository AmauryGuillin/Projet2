using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string ProfilImage { get; set; }
        public string Bio { get; set; }
        public string Games;
        public int? StuffId { get; set; }
        public virtual List<Stuff> Stuff { get; set; }
        public int? AccountId { get; set; }
        public virtual Account AccountUser { get; set; }
        public int? ChatId { get; set; }
        public virtual Chat Chat { get; set; }

    }
}
