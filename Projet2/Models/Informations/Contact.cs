using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Projet2.Models.Informations
{
    public class Contact
    {
        public int Id { get; set; }

       
        [Display(Name = "entrez votre adresse mail: ")]
        public string EmailAdress { get; set; }

      
        [Display(Name = "Entrez votre numero de telephone")]
        public string TelephoneNumber { get; set; }

    }
}
