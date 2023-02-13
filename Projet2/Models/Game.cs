using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Projet2.Models
{
    public enum GameList
    {
        [Display(Name = "MarioKart")]
        Mk,
        [Display(Name = "League of Leged")]
        Lol,
        [Display(Name = "Street Fighter")]
        Stf
    }

    public class Game
    {
		public int Id { get; set; }
        public string GameType { get; set; }
        public GameList GameList { get; set; }
    }
}