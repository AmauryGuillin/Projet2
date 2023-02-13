using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace Projet2.Models
{
    
    public enum Level
    {
        [Display(Name = "Sauron")]
        Sauron,
        [Display(Name = "Galadriel")]
        Galadriel,
        [Display(Name = "Gandalf Le Blanc")]
        Gandalf_Le_Blanc,
        [Display(Name = "Sam Gamgee Le Brave")]
        Sam_Gamgee_Le_Brave,
        [Display(Name = "Orc")]
        Orc
    }
    
    public class Coaching
    {
        public int Id { get; set; }

        public Level Level { get; set; }
    }
}
