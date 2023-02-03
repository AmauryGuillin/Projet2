﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projet2.Models
{
        public enum State
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
        public State State { get; set; }

        public int? AccountOwnerId { get; set; }
        public virtual Account AccountOwner { get; set; }

        public int? InventoryBorrowerId { get; set; }
        public virtual Inventory InventoryBorrower { get; set; }
    }
    
}
