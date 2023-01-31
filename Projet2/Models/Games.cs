using System.Collections.Generic;

namespace Projet2.Models
{
    public class Games
    {
		public int Id { get; set; }
        public string GameType { get; set; }
        public virtual List<Games> GameList { get; set; } // enum a faire
    }
}