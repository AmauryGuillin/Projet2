using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Projet2.Models
{
    /// <summary>
    /// This class represents a profile.
    /// It is connected to the account class.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Gets or sets the profile ID required by the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets the path to the profile image
        /// </summary>
        [Display(Name = "Choisissez une image de profil")]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets an IFormFile object that contains the profile image (not database-mapped).
        /// </summary>
        [NotMapped]
        public IFormFile ProfilImage { get; set; }


        //public byte[] UserPhoto { get; set; }

        //[MaxLength(50)]
        //[Required]

        /// <summary>
        /// Gets or sets the user's profile biography.
        /// </summary>
        [Display(Name = "Présentez vous en quelques lignes")]
        public string Bio { get; set; }

        //[MaxLength(30)]
        //[Required]

        /// <summary>
        /// Gets or sets the games that display on the user's profile
        /// </summary>
        [Display(Name = "Quels sont vos jeux vidéo preférés ?")]
        public string Games { get; set; }



        /// <summary>
        /// This method creates a profile
        /// </summary>
        /// <returns>Returns the created profile</returns>
        public static Profile CreateProfile()
        {
            Profile profil = new Profile {};

            return profil;
        }
    }

}
