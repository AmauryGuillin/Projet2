using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
        public enum enum_state
        {
            Neuf,
            TrèsBon,
            Bon,
            Acceptable
        }
    
    
        public class Stuff
        {
         public int Id { get; set; }
        
         [MaxLength(30)]
         [Required]
         public string Name { get; set; }
         [MaxLength(30)]
         [Required]
         public string Type { get; set; }
         public enum enum_state { Neuf, Tr }


         //public int ? prifilID
         //public virtual profil profil ...
        
        
        }
    
}
