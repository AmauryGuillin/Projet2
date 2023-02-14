using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
    /// <summary>
    /// This class represents a user's personal information that matches civil government records information
    /// </summary>
    public class InfoPerso
    {
        /// <summary>
        /// Gets or sets the personal information identifier needed by the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's name
        /// </summary>
        [MaxLength(30)]
        [Display(Name = "Nom :")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's first name
        /// </summary>
        [MaxLength(30)]
        [Display(Name = "Prénom : ")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's date of birth
        /// </summary>
        [Display(Name = "Date de naissance :")]
        public string Birthday { get; set; }


        //Regarding the genre, it's a choice because the players of jv are not categorized in a genre.
        //This information is not necessary.




    }
}
