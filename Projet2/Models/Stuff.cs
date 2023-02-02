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

        public int? ProfilId { get; set; }
        public virtual Profile Profile { get; set; }

        public int? InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }
        
        
        }
    
}
