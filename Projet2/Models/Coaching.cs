using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace Projet2.Models
{

    /// <summary>
    /// This class represents a coaching with an Employee and an Adherent
    /// </summary>
    public class Coaching
    {
        /// <summary>
        /// Gets or sets the coaching ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets the level for the adherent for the coaching.
        ///  The different types of level are provided in an enumeration.
        /// </summary>
        public Level Level { get; set; }
    }

    /// <summary>
    /// Enumeration that represents the different levels of the person registering for the activity. 
    /// </summary>
    public enum Level
    {
        /// <summary>
        /// Maximum power level representing Sauron.
        /// </summary>
        [Display(Name = "Sauron")]
        Sauron,

        /// <summary>
        /// Power level representing Galadriel.
        /// Level just below Sauron.
        /// </summary>
        [Display(Name = "Galadriel")]
        Galadriel,

        /// <summary>
        /// Power level representing Gandalf Le Blanc. 
        /// Average level.
        /// </summary>
        [Display(Name = "Gandalf Le Blanc")]
        Gandalf_Le_Blanc,

        /// <summary>
        /// Power level representing Sam Gamgee The Brave. 
        /// This level is just above orc.
        /// </summary>
        [Display(Name = "Sam Gamgee Le Brave")]
        Sam_Gamgee_Le_Brave,

        /// <summary>
        /// Minimum power level representing an Orc. 
        /// Lowest level.
        /// </summary>
        [Display(Name = "Orc")]
        Orc
    }

}
