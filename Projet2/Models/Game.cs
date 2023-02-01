using System;
using System.Collections.Generic;

namespace Projet2.Models
{
    public enum GameList
    {
        Mk,
        Lol,
        Stf
    }

    public class Game
    {
		public int Id { get; set; }
        public string GameType { get; set; }
        public GameList GameList { get; set; }
    }
}