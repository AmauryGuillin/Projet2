﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
        public enum State
        {
            Neuf,
            TrèsBon,
            Bon,
            Acceptable
        }

        public enum Type
        {
            Ordinateur,
            Console,
            PeripheriquePC,
            PeripheriqueConsole,
            AccessoireBureau,
            Salle
        }


    public class Stuff
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        //public string ImagePath { get; set; }
        //[NotMapped]
        //public IFormFile StuffImage { get; set; }

        public string Description { get; set; }
        public Type Type { get; set; }
        public State State { get; set; }

        public int? AccountOwnerId { get; set; }
        public virtual Account AccountOwner { get; set; }
        public int? InventoryBorrowerId { get; set; }
        public virtual Inventory InventoryBorrower { get; set; }
    }
    
}
