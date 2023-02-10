using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Projet2.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Display(Name = "Choisissez une image de profil")]
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ProfilImage { get; set; }


        //public byte[] UserPhoto { get; set; }

        [MaxLength(50)]
        //[Required]

        [Display(Name = "Présentez vous en quelques lignes")]
        public string Bio { get; set; }
        //[MaxLength(30)]
        //[Required]
        [Display(Name = "Quels sont vos jeux vidéo preférés ?")]
        public string Games { get; set; }
      
        
        //public int? AccountId { get; set; }
        //public virtual Account AccountUser { get; set; }
        //public int? ChatId { get; set; }
        //public virtual Messagerie Messagerie { get; set; }

        
        public static Profile CreateProfile()
        {
            Profile profil = new Profile {};

            return profil;
        }



        /////////////////////END
    }

}
