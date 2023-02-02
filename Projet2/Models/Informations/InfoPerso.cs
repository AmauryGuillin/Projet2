using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet2.Models
{
    public class InfoPerso
    {
        public int Id { get; set; }

        [MaxLength(30)]
      
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [MaxLength(30)]
       
        [Display(Name = "Prenom")]
        public string FirstName { get; set; }

        
        [Display(Name = "Date de naissance")]
        public string Birthday { get; set; }

        //rajouter le genre




    }
}
